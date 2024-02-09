using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody_;
    [SerializeField] private float acceleration_;
    [SerializeField] private float maxSpeed_;
    [SerializeField] private float jumpPower_;
    [SerializeField] private int maxAmountOfJumps_;
    [SerializeField] private Transform shield_;

    private Vector3 mousePosition_;
    public int amountOfJumps_;
    private float speed_;
    private bool isGrounded_;


    private void Awake()
    {
        amountOfJumps_ = maxAmountOfJumps_;
    }
    public void StartMovePlayer(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }

    public void OnMove(InputValue value)
    {
        speed_ = acceleration_ * value.Get<float>();
        Debug.Log(value.Get<float>());
    }

    public void OnJump(InputValue value)
    {
        if (amountOfJumps_ > 0)
        {
            playerRigidbody_.AddForce(new Vector2(playerRigidbody_.velocity.normalized.x, jumpPower_), ForceMode2D.Impulse);
            amountOfJumps_--;
        }
    }

    private void Update()
    {
        mousePosition_ = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition_.x, mousePosition_.y, Camera.main.nearClipPlane));
        worldMousePosition.z = 0f;
        // Make the transform look at the mouse position
        shield_.right = worldMousePosition - transform.position;
    }

    private void FixedUpdate()
    {
        if (playerRigidbody_.velocity.magnitude > maxSpeed_)
        {
            playerRigidbody_.velocity = Vector2.ClampMagnitude(playerRigidbody_.velocity, maxSpeed_);
        }
        playerRigidbody_.AddForce(new Vector2(speed_, playerRigidbody_.velocity.y), ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        amountOfJumps_ = maxAmountOfJumps_;
        isGrounded_ = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        isGrounded_ = false;
    }
}
