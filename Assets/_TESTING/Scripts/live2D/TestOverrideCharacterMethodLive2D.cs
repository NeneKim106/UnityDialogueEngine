using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

public class TestOverrideCharacterMethodLive2D : MonoBehaviour
{
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

        yield return new WaitForSeconds(1f);

        Mao.Hide();

        yield return new WaitForSeconds(1f);

        Mao.Show();

        yield return new WaitForSeconds(1f);

        Mao.SetColor(Color.red);

        yield return new WaitForSeconds(1f);

        Mao.TransitionColor(Color.white);

        yield return new WaitForSeconds(1f);

        Mao.UnHighlight();

        yield return new WaitForSeconds(1f);

        Mao.Highlight();

        yield return new WaitForSeconds(1f);

        Mao.FaceRight();

        yield return new WaitForSeconds(1f);

        Mao.FaceLeft();

        yield return new WaitForSeconds(1f);

        Character_Live2D Koharu = CreateCharacter("Koharu") as Character_Live2D;
        Character_Live2D Natori = CreateCharacter("Natori") as Character_Live2D;
        Character_Live2D Rice = CreateCharacter("Rice") as Character_Live2D;

        Rice.SetPosition(new Vector2(0.3f, 0));
        Mao.SetPosition(new Vector2(0.4f, 0));
        Natori.SetPosition(new Vector2(0.5f, 0));
        Koharu.SetPosition(new Vector2(0.6f, 0));

        yield return new WaitForSeconds(1f);

        CharacterManager.instance.SortCharacter(new string[] { "Koharu", "Natori", "Mao", "Rice" });

        yield return null;
    }

    // Update is called once per frame
    void Update() {

    }
}
