using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Zoo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Animal tom = new Animal();
        tom.name = "��";
        tom.sound = "�Ŀ�!";

        Animal jerry = new Animal();
        jerry.name = "����";
        jerry.sound = "����!";

        tom.PlaySound();
        jerry.PlaySound();

        jerry = tom; // �ƹ��� ������� �ʴ� ���� jerry ������ ���� ������ �÷��Ϳ��� �ڵ� �ı�
        jerry.name = "��Ű";

        tom.PlaySound();
        jerry.PlaySound();
    }

    // Update is called once per frame
    void Update() {

    }
}
