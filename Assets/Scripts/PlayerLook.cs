using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float xSens = 30f;
    [SerializeField] private float ySens = 30f;
    
    private float _xRotation = 0f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate cam rotation for up and down
        _xRotation -= (mouseY * Time.deltaTime) * ySens;
        //limit toration
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        //apply rotation to cam
        cam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);
    }
}
