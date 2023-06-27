using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMMANDS;

namespace DIALOGUE {
    public class ConversationManager {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;

        private TextArchitect architect = null;
        private bool userprompt = false;

        public ConversationManager(TextArchitect textArchitect) {
            this.architect = textArchitect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next() {
            userprompt = true;
        }

        public Coroutine StartConversation(List<string> conversation) {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));

            return process;
        }

        public void StopConversation() {
            if (!isRunning) {
                return;
            }

            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation) {
            for (int i = 0; i < conversation.Count; i++) {
                if (string.IsNullOrWhiteSpace(conversation[i])) {
                    continue;
                }
                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                if (line.hasDialogue) {
                    yield return Line_RunDialogue(line);
                }

                if (line.hasCommands) {
                    yield return Line_RunCommands(line);
                }

                if (line.hasDialogue) {
                    yield return waitForUserInput();
                }
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line) {
            if (line.hasSpeaker) {
                dialogueSystem.ShowSpeakerName(line.speakerData.displayName);
            }

            yield return BuildLineSegments(line.dialogueData);
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line) {
            List<DL_COMMAND_DATA.Command> commands = line.commandsData.commands;

            foreach (DL_COMMAND_DATA.Command command in commands) {
                if (command.waitForCompletion) {
                    yield return CommandManager.instance.Excute(command.name, command.arguments);
                }

                CommandManager.instance.Excute(command.name, command.arguments);
            }

            yield return null;
        }

        IEnumerator BuildLineSegments(DL_DIALOGUE_DATA line) {
            for (int i = 0; i < line.segments.Count; i++) {
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];

                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }

        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment) {
            switch (segment.startSignal) {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return waitForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;
            }
        }

        IEnumerator BuildDialogue(string dialogue, bool append = false) {
            if (!append) {
                architect.Build(dialogue);
            } else {
                architect.Append(dialogue);
            }

            while (architect.isBuilding) {
                if (userprompt) {
                    if (!architect.hurryUp) {
                        architect.hurryUp = true;
                    } else {
                        architect.ForceComplete();
                    }

                    userprompt = false;
                }
                yield return null;
            }
        }

        IEnumerator waitForUserInput() {
            while (!userprompt) {
                yield return null;
            }

            userprompt = false;
        }
    }
}
