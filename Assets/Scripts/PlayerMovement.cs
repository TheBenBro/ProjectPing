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
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D hitCollider_;


    private Vector3 mousePosition_;
    public int amountOfJumps_;
    private float speed_;
    private bool hittingState_ = false;


    public GameObject childObject;
    private void Awake()
    {
        hittingState_ = false;
        amountOfJumps_ = maxAmountOfJumps_;
        playerRigidbody_.centerOfMass = new Vector2(0,0);
    }

    public void OnMove(InputValue value)
    {
        speed_ = acceleration_ * value.Get<float>();
    }

    public void OnJump(InputValue value)
    {
        if (amountOfJumps_ > 0)
        {
            playerRigidbody_.AddForce(new Vector2(playerRigidbody_.velocity.normalized.x, jumpPower_), ForceMode2D.Impulse);
            amountOfJumps_--;
        }
    }

    public void OnHit(InputValue value)
    {
        playerAnimator.SetTrigger("HitTrigger");
    }

    public void EnableHit()
    {
        hittingState_ = true;
        hitCollider_.enabled = true;
    }
    public void DisableHit()
    {
        hittingState_ = false;
        hitCollider_.enabled = false;
    }

    public bool GetHitState()
    {
        return hittingState_;
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
    }
}
