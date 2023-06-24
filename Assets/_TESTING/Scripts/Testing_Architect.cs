using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DIALOGUE;

namespace TESTING {
    public class Testing_Architect : MonoBehaviour {
        DialogueSystem ds;
        TextArchitect architext;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[] {
            "                ",
            "이건 이거고ㅁㄴㅇㄻㄴㅇㄻㄴㅇㄹㄴㅁㅇㄻㄴㅇㄹㄴㅁㅇㄻㄴㅇㄹ ",
            "저건 저거고ㅁㄴㅇㄹㄴㅁㅇㄹㄴㅁㅇㄹㄴㅁㅇㄹㄴㅁㅇㄹㄴㅁㅇㄻㄴㅇㄻㄴㅇㄹ ",
            "그건 그거죠ㅁㄴㅇㄻㄴㅇㄹㄴㅁㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄹㄴㅁㅇㄹㄴㅁㅇ ",
            " 이게 맞는 건가ㅁㄴㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄹㄴㅁㅇㄹㄴㅁㅇㄹㄴㅁㅇㄻㄴㅇㄻㄴㅇ  ",
            "  그러했다고 한다ㅁㄴㅇㄹㄴㅇㄻㄴㅇㄹㄴㅁㅇㄻㄴㅇㄹㄴㅁㅇㄹㄴㅁㅇㄻㄴㅇㄹㄴㅁㅇㄹㄴㅁㅇㄹㄴㅇㅁ."
        };

        // Start is called before the first frame update
        void Start() {
            ds = DialogueSystem.instance;
            architext = new TextArchitect(ds.dialogueContainer.dialogText);
            architext.buildMethod = TextArchitect.BuildMethod.fade;
        }

        // Update is called once per frame
        void Update() {
            if (bm != architext.buildMethod) {
                architext.buildMethod = bm;
                architext.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                architext.Stop();
            }

            string longline = "ASDLFKJAOSIFDJO;SAㅁㄴ이ㅏ러;먀어ㅐ;ㄴ머ㅑㄹㅇ;냐ㅐㅁ얼;ㅣ낭ㅁ리ㅏㅓㄴㅇ린ㅇ    ";

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (architext.isBuilding) {
                    if (!architext.hurryUp) {
                        architext.hurryUp = true;
                    } else {
                        architext.ForceComplete();
                    }
                } else {
                    architext.Build(lines[Random.Range(0, lines.Length)]);
                }

            } else if (Input.GetKeyDown(KeyCode.A)) {
                architext.Append(longline);
            }
        }
    }
}
