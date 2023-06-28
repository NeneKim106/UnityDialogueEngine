using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using TMPro;

namespace TESTING {
    public class TestCharaterPrefab : MonoBehaviour {
        public TMP_FontAsset tempFont;

        // Start is called before the first frame update
        void Start() {          
            StartCoroutine(Test());
        }

        IEnumerator Test() {
            Character Elen = CharacterManager.instance.CreatCharacter("Elen");
            Character Adam = CharacterManager.instance.CreatCharacter("Adam");
            Character Ben = CharacterManager.instance.CreatCharacter("Benjamin");

            List<string> lines = new List<string>() {
                "Hi, there.",
                "My name is Elen.",
                "What's your name?",
                "Oh,{wa 1} that's very nice."
            };
            yield return Elen.Say(lines);

            Elen.SetNameColor(Color.red);
            Elen.SetDialogueColor(Color.green);
            Elen.SetNameFont(tempFont);
            Elen.SetDialogueFont(tempFont);

            yield return Elen.Say(lines);

            Elen.ResetConfigurationData();

            yield return Elen.Say(lines);

            lines = new List<string>() {
                "I'm Adam",
                "More lines{c}Here."
            };
            yield return Adam.Say(lines);

            yield return Ben.Say("This is a line that I want to say.{a} It is a simple line.");

            Debug.Log("Finished");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}