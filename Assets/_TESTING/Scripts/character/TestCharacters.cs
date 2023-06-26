using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

namespace TESTING {
    public class TestCharacters : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            Character Elen = CharacterManager.instance.CreatCharacter("Elen");
            Character Stella = CharacterManager.instance.CreatCharacter("Stella");
            Character Stella2 = CharacterManager.instance.CreatCharacter("Stella");
            Character Adam = CharacterManager.instance.CreatCharacter("Adam");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}