using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterController2D controller;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");

        controller.Move(movement);
    }
}
