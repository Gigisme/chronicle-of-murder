using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask mask;

    private Camera cam;
    private InputManager _inputManager;
    
    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        _inputManager = GetComponent<InputManager>();
    }

    public void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, mask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (_inputManager._onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
