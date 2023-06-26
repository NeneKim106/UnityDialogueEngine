using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using TMPro;

namespace DIALOGUE {
    [CreateAssetMenu(fileName = "Dialogue System Configuration", menuName = "Dialogue System/Dialogue Configuaration Asset")]
    public class DialogueSystemConfigurationSO : ScriptableObject {
        public CharacterConfigSO characterConfigurationAsset;

        public Color defualtTextColor = Color.white;
        public TMP_FontAsset defaultFont;
    }
}