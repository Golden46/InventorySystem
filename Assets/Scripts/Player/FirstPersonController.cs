using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    private bool _isSprinting;

    [Header("Functional Options")]
    public bool canInteract = true;
    public bool canLook = true;

    [Header("Controls")]
    private PlayerInputActions _playerInputActions;
    [HideInInspector] public Vector2 mouseVector;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 8.0f;

    [Header("Look Parameters")] 
    [SerializeField] private Camera playerCamera;
    private float _rotationX = 0.0f;
    [SerializeField, Range(1,180)] public float clampX = 80.0f;
    [SerializeField] public float horizontalSpeed = 20f;
    [SerializeField] public float verticalSpeed = 20f;
    [HideInInspector] public float vertInput;
    [HideInInspector] public float horInput;

    // -=[ Default gravitational pull and jump height ]=-
    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;  // Default force to apply when jumping
    [SerializeField] private float gravity = 30.0f;  // Default gravity value

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint;
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactionLayer;
    private Interactable _currentInteractable;

    private Camera _playerCamera;
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the screen
        Cursor.visible = false;  // Disable view of cursor on screen
    }

    private void Awake()
    {
        _playerCamera = Camera.main;  // Set player camera variable to the camera
        _characterController = GetComponent<CharacterController>();  // Set character controller variable to the character controller
    }

    private void Start()
    {
        _playerInputActions = PlayerInputManager.PlayerInputActions; // Get the player input actions
    }

    private void Update()
    {
        HandleMovement();
        if(canLook) HandleMouseLook();

        if(canInteract) HandleInteractionCheck();

        // Call function to apply the movements made in that frame
        ApplyFinalMovements();
    }

    private void HandleMovement()
    {
        var inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>(); // Reads the value of the player movement input

        // set currentInput variable to vector 2 sprint/walk speed * vertical axis, sprint/walk speed * horizontal axis. Which speed depends on if the user is holding the sprint key or not.
        _currentInput = new Vector2((_isSprinting ? sprintSpeed : walkSpeed) * inputVector.y, (_isSprinting ? sprintSpeed : walkSpeed) * inputVector.x);

        var moveDirectionY = _moveDirection.y;  // Create a float variable of the move direction of the users y coordinates

        _moveDirection = transform.right * _currentInput.x + -transform.forward * _currentInput.y;
        _moveDirection.y = moveDirectionY;
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        if (context.performed) _isSprinting = true; // If sprint key held, sprint
        if (context.canceled) _isSprinting = false; // If sprint key let go, don't sprint
    }
    
    public void HandleJump(InputAction.CallbackContext context)
    {
        // Raises the player a specified amount into the air if they are grounded
        if(_characterController.isGrounded)
            _moveDirection.y = jumpForce; 
    }
    
    private void HandleMouseLook()
    {
        mouseVector = _playerInputActions.Player.Look.ReadValue<Vector2>(); // Reads player mouse / joystick look

        vertInput = mouseVector.y * verticalSpeed * Time.deltaTime; // Gets the vertical input
        horInput = mouseVector.x * horizontalSpeed * Time.deltaTime; // Gets the horizontal input

        transform.Rotate(Vector3.up * horInput);
        
        _rotationX -= vertInput;
        _rotationX = Mathf.Clamp(_rotationX, -clampX, clampX);
        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 90, 0);
    }
    
    private void HandleInteractionCheck()
    {
        if(Physics.Raycast(_playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer != 6 || (_currentInteractable &&
                                                       hit.collider.gameObject.GetInstanceID() ==
                                                       _currentInteractable.GetInstanceID())) return;
            
            hit.collider.TryGetComponent(out _currentInteractable);
                
            if(_currentInteractable)
                _currentInteractable.OnFocus();
        }
        else if (_currentInteractable)
        {
            _currentInteractable.OnLoseFocus();
            _currentInteractable = null;
        }
    }

    public void HandleInteractionInput(InputAction.CallbackContext context)
    {
        if(canInteract && _currentInteractable && Physics.Raycast(_playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            _currentInteractable.OnInteract();
        }
    }
    
    private void ApplyFinalMovements()
    {   
        if (!_characterController.isGrounded)
        {
            // Starts moving the character down if he is in the air
            _moveDirection.y -= gravity * Time.deltaTime;

            // If player hits the ceiling
            if(_characterController.collisionFlags == CollisionFlags.Above)
            {
                // Stop adding velocity to the character
                _moveDirection = Vector3.zero;
                // This prevents the player from sticking
                // to the ceiling and moving along it
                _characterController.stepOffset = 0;
            }
        }
        // When the player is actually grounded
        else
        {
            if(_characterController.stepOffset == 0)
            {
                // Resets the stepOffset to the default float so
                // the character is still able to function correctly
                // while walking up steps or across bumps.
                _characterController.stepOffset = 0.3f;
            }
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}