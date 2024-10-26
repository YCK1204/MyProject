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
        // Input 처리 일반 플레이어 /
        float xInput = Input.GetAxisRaw("Horizontal");
        movementController.SetInput(xInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementController.Jump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            movementController.CreatePortal();
        }
    }
}