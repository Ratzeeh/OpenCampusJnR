using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_sprintSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool m_doubleJump = true;
    [SerializeField] private float m_gravityScale;

    public bool m_isGrounded;
    [SerializeField] private BoxCollider2D m_groundCollider;
    [SerializeField] private LayerMask m_GroundMask;

    private InputAction m_moveAction;
    private float horizontalMovement;

    private InputAction m_jumpAction;
    private readonly int m_extraJumps = 1;
    private int m_jumpsLeft;

    private void Awake()
    {
        PlayerInput m_playerInput = GetComponent<PlayerInput>();
        m_moveAction = m_playerInput.currentActionMap.FindAction("Move");
        m_jumpAction = m_playerInput.currentActionMap.FindAction("Jump");
    }

    void Start()
    {
        // Bind movement functions
        m_moveAction.started += Move;
        m_moveAction.canceled += EndMove;
    }

    void Update()
    {
        m_isGrounded = m_groundCollider.IsTouchingLayers(m_GroundMask);

        // Gravity
        if (!m_isGrounded)
            transform.position -= new Vector3(-horizontalMovement * m_moveSpeed * Time.deltaTime, m_gravityScale, 0);
        else
            transform.position -= new Vector3(-horizontalMovement * m_moveSpeed * Time.deltaTime, 0, 0);

        // Left/Right movement
        //transform.position = transform.right * horizontalMovement * m_moveSpeed * Time.deltaTime;
    }

    private void Move(InputAction.CallbackContext _ctx) => horizontalMovement = _ctx.ReadValue<Vector2>().x;

    private void EndMove(InputAction.CallbackContext _ctx) => horizontalMovement = 0;
}
