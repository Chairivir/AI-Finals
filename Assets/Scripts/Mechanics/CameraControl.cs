using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 5.0f;

    Vector3 direction;
    void Update()
    {
        float cameraSpeedModifier = 1.0f;
        float yAxis = 0.0f;

        if(Input.GetKey(KeyCode.LeftShift))
            cameraSpeedModifier = 2.0f;
        if(Input.GetKey(KeyCode.LeftControl))
            cameraSpeedModifier = 0.5f;

        if(Input.GetKey(KeyCode.Q))
            yAxis = 1.0f;
        if(Input.GetKey(KeyCode.E))
            yAxis = -1.0f;

        direction = new Vector3(Input.GetAxis("Horizontal"), yAxis, Input.GetAxis("Vertical"));

        transform.Translate(direction * Time.deltaTime * cameraSpeed * cameraSpeedModifier);
    }
}
