using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helloworld : MonoBehaviour
{
    private int count = 0; //global variable

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Hello Insi!");
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Update: " + count);
        int inc = 2; // Local variable
        count = count + inc;
    }
}