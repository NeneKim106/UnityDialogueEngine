using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS {
    [CreateAssetMenu(fileName = "Character Configuration Asset", menuName = "Dialogue System/Character Configuration Asset")]
    public class CharacterConfigSO : ScriptableObject {
        public CharacterConfigData[] charaters;

        public CharacterConfigData GetConfig(string characterName) {
            characterName = characterName.ToLower();

            for (int i = 0; i < charaters.Length; i++) {
                CharacterConfigData data = charaters[i];

                if (string.Equals(characterName, data.name.ToLower()) || string.Equals(characterName, data.alias.ToLower())) {
                    return data.Copy();
                }
            }

            return CharacterConfigData.Default;
        }
    }
}