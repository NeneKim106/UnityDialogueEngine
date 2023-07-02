using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using CHARACTERS;

namespace TESTING {
    public class TestCommandFile : MonoBehaviour {
        [SerializeField] private TextAsset fileToLoad = null;

        // Start is called before the first frame update
        void Start() {
            StartConversation();
        }

        void StartConversation() {
            GameObject prefab = Resources.Load<GameObject>("Art/testImage");
            GameObject ob = Object.Instantiate(prefab, CharacterManager.instance.characterPanel);
            ob.name = "Image";

            List<string> lines = FileManager.ReadTextAsset(fileToLoad, false);

            DialogueSystem.instance.Say(lines);
        }
    }
}