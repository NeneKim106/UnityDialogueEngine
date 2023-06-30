using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CHARACTERS {
    public class Character_Sprite : Character {
        private const string SPRITE_RENDERER_PARENT_LINE = "Renderers";
        private const string SPRITESHEET_DEFUALT_SHEETNAME = "Default";
        private const char SPRITESHEET_TEXTURE_SPRITE_DELIMITTER = '-';
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();

        public List<CharacterSpriteLayer> layers = new List<CharacterSpriteLayer>();

        private string artAssetsDirectory = "";
        public override bool isVisible {
            get { return isRevealing || rootCG.alpha == 1; }
            set { rootCG.alpha = value ? 1 : 0; }
        }

        public Character_Sprite(string name, CharacterConfigData config, GameObject prefab, string rootAssetsFolder) : base(name, config, prefab) {
            rootCG.alpha = ENABLE_ON_START ? 1 : 0;
            artAssetsDirectory = rootAssetsFolder + "/Images";

            GetLayers();

            Debug.Log($"Created Sprite Character: '{name}'");
        }

        private void GetLayers() {
            Transform rendererRoot = animator.transform.Find(SPRITE_RENDERER_PARENT_LINE);

            if (rendererRoot == null)
                return;

            for (int i = 0; i < rendererRoot.childCount; i++) {
                Transform child = rendererRoot.transform.GetChild(i);

                Image rendererImage = child.GetComponentInChildren<Image>();

                if (rendererImage != null) {
                    CharacterSpriteLayer layer = new CharacterSpriteLayer(rendererImage, i);
                    layers.Add(layer);
                    child.name = $"Layer: {i}";
                }
            }
        }

        public void SetSprite(Sprite sprite, int layer = 0) {
            layers[layer].SetSprite(sprite);
        }

        public Sprite GetSprite(string spriteName) {
            if (config.charaterType == CharacterType.SpriteSheet) {
                string[] data = spriteName.Split(SPRITESHEET_TEXTURE_SPRITE_DELIMITTER);
                Sprite[] spriteArray = new Sprite[0];

                if (data.Length == 2) {
                    string textureName = data[0];
                    spriteName = data[1];
                    spriteArray = Resources.LoadAll<Sprite>($"{artAssetsDirectory}/{textureName}");
                } else {
                    spriteArray = Resources.LoadAll<Sprite>($"{artAssetsDirectory}/{SPRITESHEET_DEFUALT_SHEETNAME}");
                }

                if (spriteArray.Length == 0)
                    Debug.LogWarning($"Character '{name}' does not have a default art asset called '{SPRITESHEET_DEFUALT_SHEETNAME}'");

                return Array.Find(spriteArray, sprite => sprite.name == spriteName);
            } else {
                Sprite sprite = Resources.Load<Sprite>($"{artAssetsDirectory}/{spriteName}");
                if (sprite == null)
                    Debug.LogWarning($"Character '{spriteName}' does not have an art");

                return sprite;
            }
        }

        public Coroutine TransitionSprite(Sprite sprite, int layer = 0, float speed = 1) {
            CharacterSpriteLayer spriteLayer = layers[layer];

            return spriteLayer.TransitionSprite(sprite, speed);
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

        public override void SetColor(Color color) {
            base.SetColor(color);
            color = displayColor;

            foreach (CharacterSpriteLayer layer in layers) {
                layer.StopChaningColor();
                layer.SetColor(color);
            }
        }

        public override IEnumerator ChangingColor(Color color, float speed) {
            foreach (CharacterSpriteLayer layer in layers) {
                layer.TransitionColor(color, speed);
            }

            yield return null;

            while (layers.Any(l => l.isChaningColor)) {
                yield return null;
            }

            co_changingColor = null;
        }

        public override IEnumerator Highlighting(bool highlighted, float speedMultiplier) {
            Color targetColor = displayColor;

            foreach (CharacterSpriteLayer layer in layers) {
                layer.TransitionColor(targetColor, speedMultiplier);
            }

            yield return null;

            while (layers.Any(l => l.isChaningColor)) {
                yield return null;
            }

            co_highlighting = null;
        }
    }
}