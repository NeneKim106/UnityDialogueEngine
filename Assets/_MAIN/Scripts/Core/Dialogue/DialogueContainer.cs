using UnityEngine;
using TMPro;

namespace DIALOGUE {
    [System.Serializable]
    public class DialogueContainer {
        public GameObject root;
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogText;

        public void SetDialogueColor(Color color) => dialogText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogText.font = font;
    }
}