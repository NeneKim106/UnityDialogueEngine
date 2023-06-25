using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class CMD_DatabaseExtension_Example : CMD_DatabaseExtension {
    new public static void Extend(CommendDatabase database) {
        // Add commmand with no parameters
        database.AddCommand("print", new Action(PrintDefualtMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUsermessage));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        // Add lambda with no parameters
        database.AddCommand("lambda", new Action(() => { Debug.Log("printing a defualt message to console from lambda command."); }));
        database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"Log User Lambda Message: '{arg}'."); }));
        database.AddCommand("lambda_mp", new Action<string[]>((arg) => { Debug.Log(string.Join(", ", arg)); }));

        // Add coroutine with no parameters
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("process_1p", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("process_mp", new Func<string[], IEnumerator>(MultiLineProcess));

        // object movement example
        database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));
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

    private static IEnumerator SimpleProcess() {
        for (int i = 0; i < 5; i++) {
            Debug.Log($"SimpleProcess - Process Running... {i}");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private static IEnumerator LineProcess(string data) {
        if (int.TryParse(data, out int num)) {
            for (int i = 0; i < num; i++) {
                Debug.Log($"LineProcess - Process Running... {i}");
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private static IEnumerator MultiLineProcess(string[] data) {
        foreach (string line in data) {
            Debug.Log($"MultiLineProcess message: '{line}'");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private static IEnumerator MoveCharacter(string direction) {
        bool left = direction.ToLower() == "left";

        Transform character = GameObject.Find("Image").transform;
        float moveSpeed = 1;
        float targetX = left ? -80 : 80;
        float currentX = character.position.x;

        Debug.Log($"Moving Start {(left ? "left" : "right")}");

        while (Mathf.Abs(targetX - currentX) > 0.1f) {
            //Debug.Log($"Moving charater to {(left ? "left" : "right")} [{currentX}/{targetX}]");
            currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed + Time.deltaTime);
            character.position = new Vector3(currentX, character.position.y, character.position.z);
            yield return null;
        }
    }
}
