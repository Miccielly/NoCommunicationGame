using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //This script is from tutorial! https://www.youtube.com/watch?v=f473C43s8nE
    [SerializeField] private float sensitivityX = 1f;
    [SerializeField] private float sensitivityY = 1f;

    private float xRotation;
    private float yRotation;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform head;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //For some reason I had to change Mouse X and Mouse Y like this to work. Don't know if it is something with my computer or what.
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        head.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
