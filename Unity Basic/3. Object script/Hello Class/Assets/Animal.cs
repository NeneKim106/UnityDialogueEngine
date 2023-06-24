using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Animal {
    // 동물에 대한 변수
    public string name;
    public string sound;

    public void PlaySound() {
        Debug.Log(name + " : " + sound);
    }
}
