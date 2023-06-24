using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class TestDialogueSegment : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartConversation();
    }

    void StartConversation() {
        List<string> lines = FileManager.ReadTextAsset("DialogueSegmentation", false);

        DialogueSystem.instance.Say(lines);
    }
}
