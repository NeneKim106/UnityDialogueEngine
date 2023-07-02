using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

public class TestAnimationLive2D : MonoBehaviour {
    public TMP_FontAsset tempFont;
    private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Test());
    }

    IEnumerator Test() {
        Character_Sprite Raelin = CreateCharacter("Raelin") as Character_Sprite;
        Character_Live2D Mao = CreateCharacter("Mao") as Character_Live2D;

        Raelin.SetPosition(new Vector2(0, 0));
        Mao.SetPosition(new Vector2(1, 0));
        yield return new WaitForSeconds(1);

        Mao.SetMotion("Healing Heart");
        yield return new WaitForSeconds(1);

        Mao.SetMotion("Proud");
        yield return new WaitForSeconds(1);

        Mao.SetMotion("Bounce");
        yield return new WaitForSeconds(1);

        yield return null;
    }

    // Update is called once per frame
    void Update() {

    }
}
