using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMouseMovement : MonoBehaviour
{
    public bool mouse, controller;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float deadzone = 0.25f;
    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (mouse)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedH * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90, 90);

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (controller)
        {

            Vector2 stickInput = new Vector2(Input.GetAxis("controllerHAxis"), Input.GetAxis("controllerVAxis"));
            if (stickInput.magnitude < deadzone)
                stickInput = Vector2.zero;

            yaw += speedH * Input.GetAxis("controllerHAxis");
            pitch -= speedH * Input.GetAxis("controllerVAxis");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
