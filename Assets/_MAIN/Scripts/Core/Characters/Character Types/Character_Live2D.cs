using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Rendering;
using Live2D.Cubism.Framework.Expression;
using System.Linq;

namespace CHARACTERS {
    public class Character_Live2D : Character {
        public const float DEFUALT_TRANSITION_SPEED = 3f;
        public const int CHARACTER_SORTING_DEPTH_SIZE = 250;

        private CubismRenderController renderController;
        private CubismExpressionController expressionController;
        private Animator motionAnimator;

        private List<CubismRenderController> oldrenders = new List<CubismRenderController>();

        private float xScale;

        public override bool isVisible { 
            get => base.isVisible || renderController.Opacity == 1; 
            set => renderController.Opacity = value ? 1 : 0; 
        }

        public Character_Live2D(string name, CharacterConfigData config, GameObject prefab, string rootAssetsFolder) : base(name, config, prefab) {
            Debug.Log($"Created Live2D Character: '{name}'");
            motionAnimator = animator.transform.GetChild(0).GetComponentInChildren<Animator>();
            renderController = motionAnimator.GetComponent<CubismRenderController>();
            expressionController = motionAnimator.GetComponent<CubismExpressionController>();

            xScale = renderController.transform.localScale.x;
        }

        public void SetMotion(string animationName) {
            motionAnimator.Play(animationName);
        }

        public void SetExpression(int expressionIndex) {
            expressionController.CurrentExpressionIndex = expressionIndex;
        }

        public void SetExpression(string expressionName) {
            expressionController.CurrentExpressionIndex = GetExpressionIndexByName(expressionName);
        }

        private int GetExpressionIndexByName(string expressionName) {
            expressionName = expressionName.ToLower();

            for (int i = 0; i < expressionController.ExpressionsList.CubismExpressionObjects.Length; i++) {
                CubismExpressionData expr = expressionController.ExpressionsList.CubismExpressionObjects[i];
                if (expr.name.Split('.')[0].ToLower() == expressionName)
                    return i;
            }

            return -1;
        }

        public override IEnumerator ShowingOrHiding(bool show) {
            float targetAlpha = show ? 1f : 0f;

            while (renderController.Opacity != targetAlpha) {
                renderController.Opacity = Mathf.MoveTowards(renderController.Opacity, targetAlpha, DEFUALT_TRANSITION_SPEED * Time.deltaTime);
                yield return null;
            }

            co_revealing = null;
            co_hiding = null;
        }

        public override void SetColor(Color color) {
            base.SetColor(color);

            foreach (CubismRenderer renderer in renderController.Renderers) {
                renderer.Color = color;
            }
        }

        public override IEnumerator ChangingColor(Color color, float speed) {
            yield return ChangingColorLive2D(color, speed);

            co_changingColor = null;
        }

        private IEnumerator ChangingColorLive2D(Color tartgetColor, float speed) {
            CubismRenderer[] renderers = renderController.Renderers;
            Color startColor = renderers[0].Color;

            float colorPecent = 0;
            
            while (colorPecent != 1) {
                colorPecent = Mathf.Clamp01(colorPecent + (DEFUALT_TRANSITION_SPEED * speed * Time.deltaTime));
                Color currentColor = Color.Lerp(startColor, tartgetColor, colorPecent);

                foreach (CubismRenderer renderer in renderController.Renderers) {
                    renderer.Color = currentColor;
                }

                yield return null;
            }
        }

        public override IEnumerator Highlighting(bool highlighted, float speedMultiplier) {
            Color tartgetColor = displayColor;

            yield return ChangingColorLive2D(tartgetColor, speedMultiplier);

            co_highlighting = null;
        }

        public override IEnumerator FaceDirection(bool faceLeft, float speedMultiplier, bool immediate) {
            GameObject newLive2DCharacter = CreateNewCharacterController();
            newLive2DCharacter.transform.localScale = new Vector3(faceLeft ? xScale : -xScale, newLive2DCharacter.transform.localScale.y, newLive2DCharacter.transform.localScale.z);
            renderController.Opacity = 0;
            float transitionSpeed = DEFUALT_TRANSITION_SPEED * speedMultiplier * Time.deltaTime;

            while (renderController.Opacity < 1 || oldrenders.Any(r => r.Opacity > 0)) {
                renderController.Opacity = Mathf.MoveTowards(renderController.Opacity, 1, transitionSpeed);

                foreach (CubismRenderController oldRenderer in oldrenders) {
                    oldRenderer.Opacity = Mathf.MoveTowards(oldRenderer.Opacity, 0, transitionSpeed);
                }

                yield return null;
            }

            foreach (CubismRenderController r in oldrenders) {
                Object.Destroy(r.gameObject);
            }

            oldrenders.Clear();

            co_flipping = null;
        }

        public GameObject CreateNewCharacterController() {
            oldrenders.Add(renderController);

            GameObject newLive2DCharacter = Object.Instantiate(renderController.gameObject, renderController.transform.parent);
            newLive2DCharacter.name = name;
            renderController = newLive2DCharacter.GetComponent<CubismRenderController>();
            expressionController = newLive2DCharacter.GetComponent<CubismExpressionController>();
            motionAnimator = newLive2DCharacter.GetComponent<Animator>();

            return newLive2DCharacter;
        }

        public override void OnSort(int sortingIndex) {
            renderController.SortingOrder = sortingIndex * CHARACTER_SORTING_DEPTH_SIZE;
        }
    }
}