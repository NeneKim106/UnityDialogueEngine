using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE {
    public class DialogueSystem : MonoBehaviour {
        [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance { get; private set; }

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake() {
            if (instance == null) {
                instance = this;
                Initialize();
            } else {
                DestroyImmediate(gameObject);
            }

        }

        public void OnUserPrompt_Next() {
            onUserPrompt_Next?.Invoke();
        }

        bool _initialized = false;
        private void Initialize() {
            if (_initialized) {
                return;
            }

            architect = new TextArchitect(dialogueContainer.dialogText);
            conversationManager = new ConversationManager(architect);
        }

        public void ShowSpeakerName(string speakerName = "") {
            if (speakerName.ToLower() != "narrator") {
                dialogueContainer.nameContainer.Show(speakerName);
            } else {
                dialogueContainer.nameContainer.Hide();
            }
        }
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();

        public void Say(string speaker, string dialogue) {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation) {
            conversationManager.StartConversation(conversation);
        }
    }
}