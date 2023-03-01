using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationX;
    public float rotationY;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY += Input.GetAxis("Mouse X") * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
