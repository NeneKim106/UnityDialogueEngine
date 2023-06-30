using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace CHARACTERS {
    public abstract class Character {
        public const bool ENABLE_ON_START = true;
        private const float UNHIGHLIGHTING_DARKEN_STRENTH = 0.65f;

        public string name = "";
        public string displayName = "";
        public RectTransform root = null;
        public CharacterConfigData config;
        public Animator animator;
        public Color color { get; protected set; } = Color.white;
        public Color displayColor => highlighted ? highlightingColor : unhighlightingColor;
        protected Color highlightingColor => color;
        protected Color unhighlightingColor => new Color(color.r * UNHIGHLIGHTING_DARKEN_STRENTH, color.g * UNHIGHLIGHTING_DARKEN_STRENTH, color.b * UNHIGHLIGHTING_DARKEN_STRENTH, color.a);
        public bool highlighted { get; protected set; } = true;

        protected CharacterManager characterManager = CharacterManager.instance;
        public DialogueSystem dialogueSystem => DialogueSystem.instance;

        protected Coroutine co_revealing, co_hiding;
        protected Coroutine co_moving;
        protected Coroutine co_changingColor;
        protected Coroutine co_highlighting;
        public bool isRevealing => co_revealing != null;
        public bool isHiding => co_hiding != null;
        public bool isMoving => co_moving != null;
        public bool isChaingColor => co_changingColor != null;
        public bool isHighlighting => (highlighted && co_highlighting != null);
        public bool isUnHighlighting => (!highlighted && co_highlighting != null);
        public virtual bool isVisible { get; set; }
            
        public Character(string name, CharacterConfigData config, GameObject prefab) {
            this.name = name;
            displayName = name;
            this.config = config;

            if (prefab != null) {
                GameObject ob = Object.Instantiate(prefab, characterManager.characterPanel);
                ob.name = characterManager.FormatCharacterPath(characterManager.characterPrefabNameFormat, name);
                ob.SetActive(true);
                root = ob.GetComponent<RectTransform>();
                animator = root.GetComponentInChildren<Animator>();
            }
        }

        public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });

        public Coroutine Say(List<string> dialogue) {
            dialogueSystem.ShowSpeakerName(displayName);
            UpdateTextCustomizationOnScreen();

            List<string> dialogueWithSpeaker = new List<string>();

            foreach (string line in dialogue) {
                string lineWithSpeaker = displayName + $" \"{line}\"";
                dialogueWithSpeaker.Add(lineWithSpeaker);
            }

            return dialogueSystem.Say(dialogueWithSpeaker);
        }
        public void UpdateTextCustomizationOnScreen() => dialogueSystem.ApplySpeakerDataToDialogueContainer(config);
        public void SetNameColor(Color color) => config.nameColor = color;
        public void SetDialogueColor(Color color) => config.dialogueColor = color;
        public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
        public void SetDialogueFont(TMP_FontAsset font) => config.dialogueFont = font;
        public void ResetConfigurationData() => config = CharacterManager.instance.GetCharacterConfigData(name);

        public virtual Coroutine Show() {
            if (isRevealing)
                return co_revealing;

            if (isHiding)
                characterManager.StopCoroutine(co_hiding);

            co_revealing = characterManager.StartCoroutine(ShowingOrHiding(true));

            return co_revealing;
        }

        public virtual Coroutine Hide() {
            if (isHiding)
                return co_hiding;

            if (isRevealing)
                characterManager.StopCoroutine(co_revealing);

            co_hiding = characterManager.StartCoroutine(ShowingOrHiding(false));

            return co_hiding;
        }

        public virtual IEnumerator ShowingOrHiding(bool show) {
            Debug.Log("Show/Hide cannot be called from a base character type.");
            yield return null;
        }

        public virtual void SetPosition(Vector2 position) {
            if (root == null)
                return;

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);

            root.anchorMin = minAnchorTarget;
            root.anchorMax = maxAnchorTarget;
        }

        public virtual Coroutine MoveToPosition(Vector2 position, float speed = 2f, bool smooth = false) {
            if (root == null)
                return null;

            if (isMoving)
                characterManager.StopCoroutine(co_moving);

            co_moving = characterManager.StartCoroutine(MovingToPosition(position, speed, smooth));

            return co_moving;
        }

        private IEnumerator MovingToPosition(Vector2 position, float speed, bool smooth) {
            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);
            Vector2 padding = root.anchorMax - root.anchorMin;

            while (root.anchorMin != minAnchorTarget || root.anchorMax != maxAnchorTarget) {
                root.anchorMin = smooth ? Vector2.Lerp(root.anchorMin, minAnchorTarget, speed * Time.deltaTime) : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed * Time.deltaTime * 0.35f);
                root.anchorMax = root.anchorMin + padding;

                if (smooth && Vector2.Distance(root.anchorMin, minAnchorTarget) <= 0.001f) {
                    root.anchorMin = minAnchorTarget;
                    root.anchorMax = maxAnchorTarget;
                    break;
                }

                yield return null;
            }

            Debug.Log("Done moving");
            co_moving = null;
        }

        protected (Vector2, Vector2) ConvertUITargetPositionToRelativeCharacterAnchorTargets(Vector2 position) {
            Vector2 padding = root.anchorMax - root.anchorMin;

            float maxX = 1f - padding.x;
            float maxY = 1f - padding.y;

            Vector2 minAnchorTarget = new Vector2(maxX * position.x, maxY * position.y);
            Vector2 maxAnchorTarget = minAnchorTarget + padding;

            return (minAnchorTarget, maxAnchorTarget);
        }

        public virtual void SetColor(Color color) {
            this.color = color;
        }

        public Coroutine TransitionColor(Color color, float speed = 1f) {
            this.color = color;

            if (isChaingColor)
                characterManager.StopCoroutine(co_changingColor);

            co_changingColor = characterManager.StartCoroutine(ChangingColor(displayColor, speed));

            return co_changingColor;
        }

        public virtual IEnumerator ChangingColor(Color color, float speed) {
            Debug.Log("Color changing is not applicable on this character type");
            yield return null;
        }

        public Coroutine Highlight(float speed = 1f) {
            if (isHighlighting)
                return co_highlighting;

            if (isUnHighlighting)
                characterManager.StopCoroutine(co_highlighting);

            highlighted = true;
            co_highlighting = characterManager.StartCoroutine(Highlighting(highlighted, speed));

            return co_highlighting;
        }

        public Coroutine UnHighlight(float speed = 1f) {
            if (isUnHighlighting)
                return co_highlighting;

            if (isHighlighting)
                characterManager.StopCoroutine(co_highlighting);

            highlighted = false;
            co_highlighting = characterManager.StartCoroutine(Highlighting(highlighted, speed));

            return co_highlighting;
        }

        public virtual IEnumerator Highlighting(bool highlighted, float speedMultiplier) {
            Debug.Log("Highlighting is not applicable on this character type");
            yield return null;
        }

        public enum CharacterType {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D,
        }
    }
}