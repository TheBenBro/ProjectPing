using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private float hitStrength_;
    public GameObject childObject;


    private PlayerMovement parent_;

    private void Awake()
    {
        if (parent_ == null)
        {
            parent_ = GetComponentInParent<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT");
        if (!collision.CompareTag("Ball")) return;
        collision.GetComponent<BallController>().HitBallHard(transform.right.normalized, hitStrength_);
    }
}
