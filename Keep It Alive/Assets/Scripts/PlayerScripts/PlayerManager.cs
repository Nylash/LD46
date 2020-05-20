using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("CONFIGURATION")]
    public float runSpeed = 40f;
    public float ladderSpeed = 15f;
    public GameObject cam1;
    public GameObject cam2;

    [Header("VARIABLES")]
    public InteractManager.Organ currentOrgan = InteractManager.Organ.none;
    public float horizontalMove = 0f;
    public float verticalMove = 0f;
    bool jump = false;
    bool onLadder = false;
    bool onLadderTrigger = false;
    bool canInteract = false;

    [Header("COMPONENTS")]
    public Canvas pauseMenu;
    public Animator animController;
    public CharacterController2D controller;
    public GameObject objectTutoScript;
    InputMap inputMap;

    private void OnEnable() => inputMap.Gameplay.Enable();
    private void OnDisable() => inputMap.Gameplay.Disable();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        inputMap = new InputMap();

        inputMap.Gameplay.xAxis.performed += ctx => horizontalMove = ctx.ReadValue<float>();
        inputMap.Gameplay.xAxis.canceled += ctx => horizontalMove = 0f;
        inputMap.Gameplay.yAxis.performed += ctx => verticalMove = ctx.ReadValue<float>();
        inputMap.Gameplay.yAxis.canceled += ctx => verticalMove = 0;
        inputMap.Gameplay.Jump.started += ctx => jump = true;
        inputMap.Gameplay.Interact.started += ctx => Interact();
        inputMap.Gameplay.SwitchCamera.started += ctx => SwitchCamera();
        inputMap.Gameplay.Pause.started += ctx => Pause();

        Time.timeScale = 1;

        controller = GetComponent<CharacterController2D>();
    }

    private void FixedUpdate()
    {
        if (!HeartManager.instance.defeat)
        {
            if (!animController.GetBool("Interacting"))
            {
                if (onLadderTrigger)
                {
                    if (verticalMove > .1f && !onLadder && Mathf.Abs(horizontalMove) < .1f)
                    {
                        onLadder = true;
                        animController.SetBool("Climbing", true);
                        animController.SetTrigger("StartClimbing");
                    }
                }
                if (onLadder)
                {
                    if (!animController.GetBool("Climbing"))
                        animController.SetBool("Climbing", true);
                    if (!jump)
                    {
                        controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, verticalMove * ladderSpeed * Time.fixedDeltaTime);
                    }
                    else
                    {
                        animController.SetBool("Climbing", false);
                        controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, jump, true);
                        jump = false;
                        onLadder = false;
                    }
                }
                else
                {
                    if (animController.GetBool("Climbing"))
                        animController.SetBool("Climbing", false);
                    if (controller.grounded && horizontalMove != 0)
                    {
                        animController.SetBool("Running", true);
                        animController.speed = Mathf.Lerp(.5f, 1f, Mathf.Abs(horizontalMove));
                    }
                    else
                    {
                        animController.SetBool("Running", false);
                        animController.speed = 1;
                    }
                    controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, jump);
                    jump = false;
                }
            }
        }
    }

    void Interact()
    {
        if (canInteract && !pauseMenu.enabled)
            InteractManager.instance.InteractWith(currentOrgan);
    }

    public void SwitchCamera()
    {
        cam1.SetActive(!cam1.activeSelf);
        cam2.SetActive(!cam2.activeSelf);
    }

    void Pause()
    {
        if (!HeartManager.instance.defeat)
        {
            UIManager.instance.Pause(!pauseMenu.enabled);      
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ladder":
                onLadderTrigger = true;
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
                onLadderTrigger = false;
                animController.SetBool("Climbing", false);
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
