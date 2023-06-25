using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandDatabase : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        CommandManager.instance.Excute("print");
    }

    void Update() {
        
    }
}
