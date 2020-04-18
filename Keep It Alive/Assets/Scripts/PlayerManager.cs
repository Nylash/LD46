using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("CONFIGURATION")]
    public float runSpeed = 40f;
    public float ladderSpeed = 15f;

    [Header("VARIABLES")]
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool onLadder = false;
    bool canInteract = false;
    public InteractManager.Organ currentOrgan = InteractManager.Organ.none;

    [Header("COMPONENTS")]
    CharacterController2D controller;
    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        inputMap = new InputMap();

        inputMap.Gameplay.xAxis.performed += ctx => horizontalMove = ctx.ReadValue<float>();
        inputMap.Gameplay.xAxis.canceled += ctx => horizontalMove = 0f;
        inputMap.Gameplay.yAxis.performed += ctx => verticalMove = ctx.ReadValue<float>();
        inputMap.Gameplay.yAxis.canceled += ctx => verticalMove = 0;
        inputMap.Gameplay.Jump.started += ctx => jump = true;
        inputMap.Gameplay.Interact.started += ctx => Interact();

        controller = GetComponent<CharacterController2D>();
    }

    private void FixedUpdate()
    {
        if (onLadder)
        {
            if (!jump)
                controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, verticalMove * ladderSpeed * Time.fixedDeltaTime);
            else
            {
                controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, jump, true);
                jump = false;
                onLadder = false;
            } 
        }
        else
        {
            controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }

    void Interact()
    {
        if (canInteract)
            InteractManager.instance.InteractWith(currentOrgan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ladder":
                onLadder = true;
                break;
            case "Organ":
                currentOrgan = collision.gameObject.GetComponent<OrganTrigger>().organ;
                canInteract = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ladder":
                onLadder = false;
                break;
            case "Organ":
                canInteract = false;
                currentOrgan = InteractManager.Organ.none;
                break;
            default:
                break;
        }
    }
}
