using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class TestCommandSystem : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            StartConversation();
        }

        void StartConversation() {
            List<string> lines = FileManager.ReadTextAsset("CommandSystem", false);

            foreach (string line in lines) {
                if (string.IsNullOrWhiteSpace(line)) {
                    continue;
                }

                DIALOGUE_LINE dl = DialogueParser.Parse(line);
                if (dl.commandsData == null) {
                    continue;
                }

                for (int i = 0; i < dl.commandsData.commands.Count; i++) {
                    DL_COMMAND_DATA.Command command = dl.commandsData.commands[i];
                    Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
                }
            }

            //DialogueSystem.instance.Say(lines);
        }
    }
}