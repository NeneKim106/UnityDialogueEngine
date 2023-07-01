using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;

namespace TESTING {
    public class TestCharacterColor : MonoBehaviour {
        public TMP_FontAsset tempFont;
        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(Test());
        }

        IEnumerator Test() {
            Character_Sprite Raelin = CreateCharacter("raelin") as Character_Sprite;

            yield return new WaitForSeconds(1);

            yield return Raelin.TransitionColor(Color.red, 0.3f);
            yield return Raelin.TransitionColor(Color.blue, 0.5f);
            yield return Raelin.TransitionColor(Color.yellow, 0.3f);
            yield return Raelin.TransitionColor(Color.white, 0.1f);

            yield return new WaitForSeconds(1);

            Raelin.layers[1].SetColor(Color.red);

            yield return new WaitForSeconds(1);

            Raelin.SetColor(Color.red);

            yield return new WaitForSeconds(1);

            Raelin.SetColor(Color.white);

            yield return null;
        }

        // Update is called once per frame
        void Update() {

        }
    }
}