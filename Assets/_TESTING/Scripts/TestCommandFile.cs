using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class TestCommandFile : MonoBehaviour {
    [SerializeField] private TextAsset fileToLoad = null;

    // Start is called before the first frame update
    void Start() {
        StartConversation();
    }

    void StartConversation() {
        List<string> lines = FileManager.ReadTextAsset(fileToLoad, false);

        DialogueSystem.instance.Say(lines);
    }
}
