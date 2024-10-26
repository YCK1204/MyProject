using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerController : MonoBehaviour
{
    public MovementController movementController;

    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        // Input Ã³¸®
        float xInput = Input.GetAxisRaw("Horizontal");
        movementController.SetInput(xInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementController.Jump();
        }
    }
}