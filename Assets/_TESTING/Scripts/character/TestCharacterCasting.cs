using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using TMPro;
using UnityEngine;

public class TestCharacterCasting : MonoBehaviour {
    public TMP_FontAsset tempFont;
    private Character CreatCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

    // Start is called before the first frame update
    void Start() {
        //Character FemaleStudent2 = CharacterManager.instance.CreatCharacter("Female student 2");
        //Character Raelin = CharacterManager.instance.CreatCharacter("Raelin");
        //Character Generic = CharacterManager.instance.CreatCharacter("Generic");

        StartCoroutine(Test());
    }

    IEnumerator Test() {
        Character guard1 = CreatCharacter("Guard1 as Generic"); 
        Character guard2 = CreatCharacter("Guard2 as Generic"); 
        Character guard3 = CreatCharacter("Guard3 as Generic");

        guard1.Show();
        guard2.Show();
        guard3.Show();

        guard1.SetDialogueFont(tempFont);
        guard1.SetNameFont(tempFont);
        guard2.SetDialogueColor(Color.cyan);
        guard3.SetNameColor(Color.red);

        yield return guard1.Say("I want to say something important.");
        yield return guard2.Say("Hold your peace..");
        yield return guard3.Say("Let hime speak..");
    }

    // Update is called once per frame
    void Update() {

    }
}
