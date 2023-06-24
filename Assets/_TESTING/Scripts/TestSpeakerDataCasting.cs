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

            Debug.Log($"{dl.speakerData.name} as [{(dl.speakerData.castName != string.Empty ? dl.speakerData.castName : dl.speakerData.name)}] at {dl.speakerData.castPosition}");

            List<(int l, string ex)> expr = dl.speakerData.CastExpressions;
            for (int c = 0; c < expr.Count; c++) {
                Debug.Log($"[Layer[{expr[c].l}] = '{expr[c].ex}']");
            }
        }

        DialogueSystem.instance.Say(lines);
    }
}
