using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example2 : example
{

    new public void Start()
    {
        printHello();
    }

    new void printHello()
    {
        Debug.Log("Bye");
    }

    public override void printSomething()
    {
        Debug.Log("Something More");
    }
}
