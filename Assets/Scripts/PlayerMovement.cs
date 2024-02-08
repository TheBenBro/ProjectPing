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

    private float speed_;

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
        playerRigidbody_.AddForce(new Vector2(playerRigidbody_.velocity.normalized.x, 5), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (playerRigidbody_.velocity.magnitude > maxSpeed_)
        {
            playerRigidbody_.velocity = Vector2.ClampMagnitude(playerRigidbody_.velocity, maxSpeed_);
        }
        playerRigidbody_.AddForce(new Vector2(speed_, playerRigidbody_.velocity.y), ForceMode2D.Force);

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("hit");
    //}
}
