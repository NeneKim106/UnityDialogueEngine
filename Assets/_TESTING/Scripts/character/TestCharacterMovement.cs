using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

namespace TESTING {
    public class TestCharacterMovement : MonoBehaviour {
        public TMP_FontAsset tempFont;
        private Character CreatCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start() {
            //Character FemaleStudent2 = CharacterManager.instance.CreatCharacter("Female student 2");
            //Character Raelin = CharacterManager.instance.CreatCharacter("Raelin");
            //Character Generic = CharacterManager.instance.CreatCharacter("Generic");

            StartCoroutine(Test());
        }

        IEnumerator Test() {
            Character guard = CreatCharacter("Guard1 as Generic");
            Character Raelin = CreatCharacter("Raelin");
            Character Chisato = CreatCharacter("Chisato");
            Character Student = CreatCharacter("Female Student 2");

            guard.SetPosition(Vector2.zero);
            Raelin.SetPosition(new Vector2(0.5f, 0.5f));
            Chisato.SetPosition(Vector2.one);
            Student.SetPosition(new Vector2(2f, 1f));

            Raelin.Show();
            Chisato.Show();
            Student.Show();

            yield return guard.Show();
            yield return guard.MoveToPosition(Vector2.one, smooth: true);
            yield return guard.MoveToPosition(Vector2.zero, smooth: true);


            guard.SetDialogueFont(tempFont);
            guard.SetNameFont(tempFont);
            Raelin.SetDialogueColor(Color.cyan);
            Chisato.SetNameColor(Color.red);

            yield return guard.Say("I want to say something important.");
            yield return Raelin.Say("Hold your peace..");
            yield return Chisato.Say("Let hime speak..");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}