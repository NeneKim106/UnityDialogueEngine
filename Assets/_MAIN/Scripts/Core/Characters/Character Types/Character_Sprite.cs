using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS {
    public class Character_Sprite : Character {
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();

        public Character_Sprite(string name, CharacterConfigData config, GameObject prefab) : base(name, config, prefab) {
            Debug.Log($"Created Sprite Character: '{name}'");
        }

        public override IEnumerator ShowingOrHiding(bool show) {
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;
            
            while (self.alpha != targetAlpha) {
                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, 3f * Time.deltaTime);
                yield return null;
            }

            co_hiding = null;
            co_revealing = null;
        }
    }
}