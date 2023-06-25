using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DatabaseExtension_Example : CMD_DatabaseExtension {
    new public static void Extend(CommendDatabase database) {
        // Add commmand with no parameters
        database.AddCommand("print", new Action(PrintDefualtMessage));
    }

    private static void PrintDefualtMessage() {
        Debug.Log("printing a defualt message to console");
    }
}
