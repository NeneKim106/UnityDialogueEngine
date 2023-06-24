using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Zoo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Animal tom = new Animal();
        tom.name = "톰";
        tom.sound = "냐옹!";

        Animal jerry = new Animal();
        jerry.name = "제리";
        jerry.sound = "찍찍!";

        tom.PlaySound();
        jerry.PlaySound();

        jerry = tom; // 아무도 사용하지 않는 이전 jerry 참조는 이후 가비지 컬렉터에서 자동 파괴
        jerry.name = "미키";

        tom.PlaySound();
        jerry.PlaySound();
    }

    // Update is called once per frame
    void Update() {

    }
}
