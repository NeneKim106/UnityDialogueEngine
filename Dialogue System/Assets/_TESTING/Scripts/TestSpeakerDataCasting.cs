using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class TestSpeakerDataCasting : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartConversation();
    }

    void StartConversation() {
        List<string> lines = FileManager.ReadTextAsset("SpeakerDataCast", false);

        for (int i = 0; i < lines.Count; i++) {
            string line = lines[i];

            if (string.IsNullOrEmpty(line))
                continue;

            DIALOGUE_LINE dl = DialogueParser.Parse(line);

            Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}] at {dl.speaker.castPosition}");

            List<(int l, string ex)> expr = dl.speaker.CastExpressions;
            for (int c = 0; c < expr.Count; c++) {
                Debug.Log($"[Layer[{expr[c].l}] = '{expr[c].ex}']");
            }
        }

        DialogueSystem.instance.Say(lines);
    }
}
