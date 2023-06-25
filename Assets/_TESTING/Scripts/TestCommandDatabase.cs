using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandDatabase : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        CommandManager.instance.Excute("print");
        CommandManager.instance.Excute("print_1p", "Hello World");
        CommandManager.instance.Excute("print_mp", "Line 1", "Line 2", "Line 3");
    }

    void Update() {
        
    }
}
