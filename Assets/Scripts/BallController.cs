using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRIgidBody2d_;

    [SerializeField] float speed_;

    private float directionX;
    private float directionY;
    // Start is called before the first frame update
    void Start()
    {
        ballRIgidBody2d_.velocity = Random.insideUnitCircle.normalized * speed_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballRIgidBody2d_.velocity = (collision.GetContact(0).normal + ballRIgidBody2d_.velocity.normalized) * speed_;
    }
}
