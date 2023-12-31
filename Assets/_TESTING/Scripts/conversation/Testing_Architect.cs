using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class Testing_Architect : MonoBehaviour {
        DialogueSystem ds;
        TextArchitect architext;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[] {
            "                ",
            "戚闇 戚暗壱けいしかいしかいしぉいけしかいしぉいけしかいしぉ ",
            "煽闇 煽暗壱けいしぉいけしぉいけしぉいけしぉいけしぉいけしかいしかいしぉ ",
            "益闇 益暗倉けいしかいしぉいけしかいしかいしかいしかいしかいしかいしぉいけしぉいけし ",
            " 戚惟 限澗 闇亜けいしかいしかいしかいしかいしぉいけしぉいけしぉいけしかいしかいし  ",
            "  益君梅陥壱 廃陥けいしぉいしかいしぉいけしかいしぉいけしぉいけしかいしぉいけしぉいけしぉいしけ."
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

            string longline = "ASDLFKJAOSIFDJO;SAけい戚た君;枯嬢だ;い袴ちぉし;劃だけ杖;び涯け軒たっいし鍵し    ";

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
