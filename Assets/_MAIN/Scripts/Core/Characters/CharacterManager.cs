using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DIALOGUE;
using UnityEngine;

namespace CHARACTERS {
    public class CharacterManager : MonoBehaviour {
        public static CharacterManager instance { get; private set; }
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigurationAsset;

        private const string CHARACTER_CASTING_ID = " as ";
        private const string CHARACTER_NAME_ID = "<charname>";
        public string characterRootPathFormat => $"Characters/{CHARACTER_NAME_ID}";
        public string characterPrefabNameFormat => $"Character - [{CHARACTER_NAME_ID}]";
        public string characterPrefab => $"{characterRootPathFormat}/{characterPrefabNameFormat}";

        [SerializeField] private RectTransform _characterpanel = null;
        [SerializeField] private RectTransform _characterpanel_live2D = null;
        [SerializeField] private RectTransform _characterpanel_model3D = null;
        public RectTransform characterPanel => _characterpanel;
        public RectTransform characterPanelLive2D => _characterpanel_live2D;
        public RectTransform characterPanelModel3D => _characterpanel_model3D;

        private void Awake() {
            instance = this;
        }

        public CharacterConfigData GetCharacterConfigData(string characterName) {
            return config.GetConfig(characterName);
        }

        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false) {
            if (characters.ContainsKey(characterName.ToLower())) {
                return characters[characterName.ToLower()];
            } else if (createIfDoesNotExist) {
                return CreateCharacter(characterName);
            }

            return null;
        }

        public Character CreateCharacter(string characterName) {
            if (characters.ContainsKey(characterName.ToLower())) {
                Debug.LogWarning($"A Character called '{characterName}' already exists. Did not create the character");
                return null;
            }

            CHARACTER_INFO info = GetCharacterInfo(characterName);

            Character character = CreateCharacterFromInfo(info);

            characters.Add(info.name.ToLower(), character);

            return character;
        }

        private CHARACTER_INFO GetCharacterInfo(string characterName) {
            CHARACTER_INFO result = new CHARACTER_INFO();

            string[] nameData = characterName.Split(CHARACTER_CASTING_ID, System.StringSplitOptions.RemoveEmptyEntries);
            result.name = nameData[0];
            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;
            result.config = config.GetConfig(result.castingName);
            result.prefab = GetPrefabForCharacter(result.castingName);
            result.rootCharacterFoler = FormatCharacterPath(characterRootPathFormat, result.castingName);

            return result;
        }

        private GameObject GetPrefabForCharacter(string characterName) {
            string prefabPath = FormatCharacterPath(characterPrefab, characterName);
            return Resources.Load<GameObject>(prefabPath);
        }

        public string FormatCharacterPath(string path, string characterName) => path.Replace(CHARACTER_NAME_ID, characterName);

        private Character CreateCharacterFromInfo(CHARACTER_INFO info) {
            CharacterConfigData config = info.config;

            switch (config.charaterType) {
                case Character.CharacterType.Text:
                    return new Character_Text(info.name, config);
                case Character.CharacterType.Sprite:
                case Character.CharacterType.SpriteSheet:
                    return new Character_Sprite(info.name, config, info.prefab, info.rootCharacterFoler);
                case Character.CharacterType.Live2D:
                    return new Character_Live2D(info.name, config, info.prefab, info.rootCharacterFoler);
                case Character.CharacterType.Model3D:
                    return new Character_Model3D(info.name, config, info.prefab, info.rootCharacterFoler);
                default:
                    return null;
            }
        }

        public void SortCharacter() {
            List<Character> activeCharaters = characters.Values.Where(c => c.root.gameObject.activeInHierarchy && c.isVisible).ToList();
            List<Character> inactiveCharaters = characters.Values.Except(activeCharaters).ToList();

            activeCharaters.Sort((a, b) => a.priority.CompareTo(b.priority));
            activeCharaters.Concat(inactiveCharaters);

            SortCharacter(activeCharaters);
        }

        public void SortCharacter(string[] chracterNames) {
            List<Character> sortedCharacters = new List<Character>();

            sortedCharacters = chracterNames
                .Select(name => GetCharacter(name))
                .Where(character => character != null)
                .ToList();

            List<Character> remainingCharacters = characters.Values
                .Except(sortedCharacters)
                .OrderBy(character => character.priority)
                .ToList();

            sortedCharacters.Reverse();

            int startingPriority = remainingCharacters.Count > 0 ? remainingCharacters.Max(c => c.priority) : 0;
            for (int i = 0; i < sortedCharacters.Count; i++) {
                Character character = sortedCharacters[i];
                character.SetPriority(startingPriority + i + 1, autoSortCharacterOnUI: false);
            }

            List<Character> allCharacters = remainingCharacters.Concat(sortedCharacters).ToList();
            SortCharacter(allCharacters);
        }

        private void SortCharacter(List<Character> chracterSortingOrder) {
            int i = 0;
            foreach (Character character in chracterSortingOrder) {
                character.root.SetSiblingIndex(i++);
            }
        }

        private class CHARACTER_INFO {
            public string name = "";
            public string castingName = "";

            public string rootCharacterFoler = "";

            public CharacterConfigData config = null;

            public GameObject prefab = null;
        }
    }
}