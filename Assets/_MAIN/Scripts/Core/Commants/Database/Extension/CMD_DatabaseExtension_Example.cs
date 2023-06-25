using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DatabaseExtension_Example : CMD_DatabaseExtension {
    new public static void Extend(CommendDatabase database) {
        // Add commmand with no parameters
        database.AddCommand("print", new Action(PrintDefualtMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUsermessage));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        // Add lambda with no parameters

        // Add coroutine with no parameters
    }

    private static void PrintDefualtMessage() {
        Debug.Log("printing a defualt message to console");
    }

    private static void PrintUsermessage(string message) {
        Debug.Log($"User Message: '{message}'");
    }

    private static void PrintLines(string[] lines) {
        int i = 1;
        foreach (string line in lines) {
            Debug.Log($"{i++}. '{line}'");
        }
    }
}