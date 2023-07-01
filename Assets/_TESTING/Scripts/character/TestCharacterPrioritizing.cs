using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

public class TestCharacterPrioritizing : MonoBehaviour {
    public TMP_FontAsset tempFont;
    private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Test());
    }

    IEnumerator Test() {
        Character_Sprite Guard = CreateCharacter("Guard as Generic") as Character_Sprite;
        Character_Sprite GuardRed = CreateCharacter("GuardRed as Generic") as Character_Sprite;
        Character_Sprite Raelin = CreateCharacter("raelin") as Character_Sprite;
        Character_Sprite Chisato = CreateCharacter("Chisato") as Character_Sprite;

        GuardRed.SetColor(Color.red);

        Raelin.SetPosition(new Vector2(0.3f, 0));
        Chisato.SetPosition(new Vector2(0.45f, 0));
        Guard.SetPosition(new Vector2(0.6f, 0));
        GuardRed.SetPosition(new Vector2(0.75f, 0));

        yield return new WaitForSeconds(1);

        Raelin.SetPriority(10);
        Chisato.SetPriority(5);
        Guard.SetPriority(20);
        GuardRed.SetPriority(200);

        yield return new WaitForSeconds(1);

        CharacterManager.instance.SortCharacter(new string[] { "Chisato", "Raelin" });

        yield return new WaitForSeconds(1);

        CharacterManager.instance.SortCharacter(new string[] { "Raelin", "Guard", "GuardRed", "Chisato" });

        yield return null;
    }

    // Update is called once per frame
    void Update() {

    }
}
