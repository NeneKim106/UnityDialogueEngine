using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using TMPro;
using Unity.VisualScripting;

namespace TESTING {
    public class TestCharacterPrefab : MonoBehaviour {
        public TMP_FontAsset tempFont;

        // Start is called before the first frame update
        void Start() {
            //Character FemaleStudent2 = CharacterManager.instance.CreatCharacter("Female student 2");
            //Character Raelin = CharacterManager.instance.CreatCharacter("Raelin");
            //Character Generic = CharacterManager.instance.CreatCharacter("Generic");

            StartCoroutine(Test());
        }

        IEnumerator Test() {
            Character Chisato = CharacterManager.instance.CreatCharacter("Chisato");

            yield return new WaitForSeconds(1);
            
            yield return Chisato.Show();
            yield return Chisato.Say("¾È³É");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}