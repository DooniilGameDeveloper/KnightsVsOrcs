using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed;
    void Update()
    {
        var translate = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, 0);
        transform.Translate(translate * Time.deltaTime);
    }
}
