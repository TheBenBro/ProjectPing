using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidBody2d_;

    [SerializeField] float speed_;

    private float directionX;
    private float directionY;
    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody2d_.velocity = Random.insideUnitCircle.normalized * speed_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitBallSoft(Vector2 direction, float hitStrength)
    {
        ballRigidBody2d_.velocity = direction * speed_;
    }

    public void HitBallHard(Vector2 direction, float hitStrength)
    {
        ballRigidBody2d_.velocity = direction * speed_ * hitStrength;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            ballRigidBody2d_.velocity = (collision.GetContact(0).normal + ballRigidBody2d_.velocity.normalized).normalized * speed_;
        }
    }
}
