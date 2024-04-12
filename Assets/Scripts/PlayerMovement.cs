using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Players Stats
    private PlayerStats playerStats;

    public bool isMoving;

    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 movement;

    private Animator animator;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x == 0 && movement.y == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        animator.SetBool("isMoving", isMoving);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerStats.speed * Time.fixedDeltaTime);
    }
}
