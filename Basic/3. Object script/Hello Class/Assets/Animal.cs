using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Animal {
    // ������ ���� ����
    public string name;
    public string sound;

    public void PlaySound() {
        Debug.Log(name + " : " + sound);
    }
}
