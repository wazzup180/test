using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerarig : MonoBehaviour
{
    public float Speed = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()

    {
        // get input- this will increase the number in your console if you press your left joystick else itll stay static
        // + Also remeber that in unity y is the vertical axis unlike z in Rhino
        Vector3 joystick = new Vector3(Input.GetAxis("LeftJoyX"), 0, Input.GetAxis("LeftJoyY"));

        // camera vectors
        Vector3 forward = Camera.main.transform.forward;
        Debug.DrawRay(transform.position, forward * 15, Color.grey);
        Vector3 project = Vector3.ProjectOnPlane(forward, Vector3.up);
        Debug.DrawRay(transform.position, project * 15, Color.blue);

        Vector3 right = Camera.main.transform.right;
        Debug.DrawRay(transform.position, right * 15, Color.red);

        //only continue if joystick is pressed more than 0.3f
        if (joystick.magnitude < 0.3f) { return; }
        Debug.Log("Camera Move");

        //move camera
        Vector3 move = right * joystick.x + project * -joystick.z;
        transform.Translate(move.normalized * Time.deltaTime * Speed);
        Debug.DrawRay(transform.position, move, Color.yellow);
    }
}