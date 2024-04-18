using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Players Stats
    private PlayerStats playerStats;

    public bool isMoving;

    //Game Compnent
    public Rigidbody2D rb;
    public Camera cam;
    private TrailRenderer trailRenderer;

    private Vector2 movement;

    private Animator animator;

    //DASH
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    private bool isDashing;
    private bool canDash;

    private void Awake()
    {
        this.playerStats = GetComponent<PlayerStats>();
        this.animator = GetComponent<Animator>();
        this.trailRenderer = transform.Find("Trail").gameObject.GetComponent<TrailRenderer>();
        this.isDashing = false;
        this.canDash = true;
    }

    void Update()
    {

        if (this.isDashing)
        {
            return;
        }
        
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

        if(Input.GetKeyDown(KeyCode.Space) && this.canDash)
        {
            StartCoroutine(Dash());
        }

    }

    void FixedUpdate()
    {
        if (this.isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(movement.x * playerStats.speed, movement.y * playerStats.speed);
        //rb.MovePosition(rb.position + movement * playerStats.speed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        this.trailRenderer.emitting = true;
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        this.trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
