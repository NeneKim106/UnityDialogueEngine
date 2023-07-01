using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

public class TestCharacterHighlighting : MonoBehaviour {
    public TMP_FontAsset tempFont;
    private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Test());
    }

    IEnumerator Test() {
        Character_Sprite Chisato = CreateCharacter("Chisato") as Character_Sprite;
        Character_Sprite Raelin = CreateCharacter("raelin") as Character_Sprite;

        Chisato.SetPosition(new Vector2(1, 0));
        Raelin.SetPosition(Vector2.zero);

        Chisato.UnHighlight();
        yield return Raelin.Say("I want to say something.");

        Raelin.UnHighlight();
        Chisato.Highlight();
        yield return Chisato.Say("But I want to say something too.");

        Chisato.UnHighlight();
        Raelin.Highlight();
        yield return Raelin.Say("Sure,{a} be my guest.");

        Raelin.UnHighlight();
        Chisato.Highlight();
        yield return Chisato.Say("¾È³É");
    }

    // Update is called once per frame
    void Update() {

    }
}
