using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;

namespace TESTING {
    public class TestCharacterSprite : MonoBehaviour {
        public TMP_FontAsset tempFont;
        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start() {
            //Character FemaleStudent2 = CharacterManager.instance.CreatCharacter("Female student 2");
            //Character Raelin = CharacterManager.instance.CreatCharacter("Raelin");
            //Character Generic = CharacterManager.instance.CreatCharacter("Generic");

            StartCoroutine(Test());
        }

        IEnumerator Test() {
            Character_Sprite Guard = CreateCharacter("Guard as Generic") as Character_Sprite;
            Character_Sprite Chisato = CreateCharacter("Chisato") as Character_Sprite;
            Character_Sprite Raelin = CreateCharacter("raelin") as Character_Sprite;
            Guard.isVisible = false;
            Chisato.isVisible = false;
            Raelin.isVisible = false;

            yield return new WaitForSeconds(1);

            Sprite chisatoBodySprite = Chisato.GetSprite("1");
            Sprite chisatoAccesorySprite = Chisato.GetSprite("star 1");

            Chisato.SetSprite(chisatoBodySprite, 0);
            Chisato.SetSprite(chisatoAccesorySprite, 1);
            Chisato.SetPosition(new Vector2(0.5f, 0));

            Sprite raelinBody = Raelin.GetSprite("B2");
            Sprite raelinFace = Raelin.GetSprite("B_Laugh");
            yield return Raelin.TransitionSprite(raelinBody, layer:0);
            yield return Raelin.TransitionSprite(raelinFace, layer:1);
            Raelin.SetPosition(new Vector2(0.5f, 0));

            Chisato.Show();
            Raelin.Show();
            
            Chisato.MoveToPosition(new Vector2(1, 0));
            yield return Raelin.MoveToPosition(new Vector2(0, 0));

            yield return Raelin.TransitionSprite(Raelin.GetSprite("B_Scold"), layer:1);

            Guard.SetPosition(new Vector2(0.5f, 0));
            yield return Guard.Show();

            yield return new WaitForSeconds(1);
            
            Sprite s1 = Guard.GetSprite("Monk");
            Guard.TransitionSprite(s1);

            yield return Guard.Say("¾È³É");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}