using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions m_Actions;
    CharacterController cc;

    [Header("Jump Settiings")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float timeToJumpApex = 0.4f;

    private float gravity;
    private float initalJumpVelocity;

    private Vector2 moveInput = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    private bool jumpPressed = false;

    #region Input Handling
    void Awake()
    {
        m_Actions = new InputSystem_Actions();
        m_Actions.Player.SetCallbacks(this);
        m_Actions.Player.Enable();
    }

    void OnDestroy()
    {
        m_Actions.Dispose();
    }

    void OnEnable()
    {
        m_Actions.Enable();
    }

    void OnDisable()
    {
        m_Actions.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
            return;
        }

        moveInput = Vector2.zero;
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();


        CalculateJumpVariables();
    }

    //this triggers when a value is changed in the inspector
    void OnValidate()
    {
        CalculateJumpVariables();
    }

    void CalculateJumpVariables()
    {

        try
        {
            if (timeToJumpApex <= 0)
                throw new System.ArgumentOutOfRangeException("timeToJumpApex must be greater than zero.");

            if (jumpHeight <= 0)
                throw new System.ArgumentOutOfRangeException("jumpHeight must be greater than zero.");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError(e.Message);
            timeToJumpApex = 0.4f;
            jumpHeight = 2f;
        }
        


        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        initalJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCharacterVelocity();

        cc.Move(velocity * Time.fixedDeltaTime);
    }

    void UpdateCharacterVelocity()
    {
        velocity.x = moveInput.x * 5f;
        velocity.z = moveInput.y * 5f;

        if (cc.isGrounded)
        {
            velocity.y = -cc.skinWidth;
            if (jumpPressed)
            {
                velocity.y = initalJumpVelocity;
            }
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
    }
}
