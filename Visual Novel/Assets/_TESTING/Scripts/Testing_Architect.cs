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
            "�̰� �̰Ű����������������������������������������������� ",
            "���� ���Ű����������������������������������������������������������� ",
            "�װ� �װ��Ҥ����������������������������������������������������������������������� ",
            " �̰� �´� �ǰ�������������������������������������������������������������������  ",
            "  �׷��ߴٰ� �Ѵ٤�����������������������������������������������������������������������������."
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

            string longline = "ASDLFKJAOSIFDJO;SA�����̤���;�Ͼ��;���Ӥ�����;�Ĥ�����;�ӳ��������ä�������    ";

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
