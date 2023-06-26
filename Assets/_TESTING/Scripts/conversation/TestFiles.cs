using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING {
    public class TestFiles : MonoBehaviour {
        //[SerializeField] private TextAsset fileName;
        private string fileName = "testFile";
        //private string fileName = "testFile.txt";

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(Run());
        }

        IEnumerator Run() {
            List<string> lines = FileManager.ReadTextAsset(fileName, false);
            //List<string> lines = FileManager.ReadTextfile(fileName, false);

            foreach (string line in lines) {
                Debug.Log(line);
            }

            yield return null;
        }
    }
}