using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandDatabase : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Running());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            CommandManager.instance.Excute("moveCharDemo", "left");
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            CommandManager.instance.Excute("moveCharDemo", "right");
        }
    }

    IEnumerator Running() {
        yield return CommandManager.instance.Excute("print");
        yield return CommandManager.instance.Excute("print_1p", "Hello World");
        yield return CommandManager.instance.Excute("print_mp", "Line 1", "Line 2", "Line 3");

        yield return CommandManager.instance.Excute("lambda");
        yield return CommandManager.instance.Excute("lambda_1p", "Hello World");
        yield return CommandManager.instance.Excute("lambda_mp", "Line 1", "Line 2", "Line 3");

        yield return CommandManager.instance.Excute("process");
        yield return CommandManager.instance.Excute("process_1p", "3");
        yield return CommandManager.instance.Excute("process_mp", "Process Line 1", "Process Line 2", "Process Line 3");
    }
}
