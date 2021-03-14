using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MouseLook : NetworkBehaviour
{
    private float xRotation = 0f;

    public Transform playerBody;
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    
}
