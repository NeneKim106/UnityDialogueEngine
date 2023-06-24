using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HelloCode : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        int[] students = new int[5];
        students[0] = 100;
        students[1] = 90;
        students[2] = 80;
        students[3] = 70;
        students[4] = 60;
        for (int i = 0; i < students.Length; i++) {
            Debug.Log((i + 1) + " students score: " + students[i]);
        }
    }

    float GetDistance(float x1, float y1, float x2, float y2) {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

    // Update is called once per frame
    void Update() {

    }
}
