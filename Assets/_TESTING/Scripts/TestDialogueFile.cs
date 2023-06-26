using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class TestDialogueFile : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            StartConversation();
        }

        void StartConversation() {
            List<string> lines = FileManager.ReadTextAsset("CommandDatabase", false);

            DialogueSystem.instance.Say(lines);
        }
    }
}