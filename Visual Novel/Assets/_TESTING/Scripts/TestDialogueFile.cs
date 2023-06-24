using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class TestDialogueFile : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartConversation();
    }

    void StartConversation() {
        List<string> lines = FileManager.ReadTextAsset("testFile2", false);
        //List<string> lines = FileManager.ReadTextAsset("DialogueSegmentation", false);

        DialogueSystem.instance.Say(lines);
    }
}
