using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Inputs
    public GameObject options;
    private InputBinding inputs;

    //AUDIO
    public GameObject AudioManager;

    public float playRate = 5f;
    private float nextTimeToPlay = 0f;

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
        this.inputs = options.GetComponent<InputBinding>();
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

        HandleMoving();

        if (movement.x == 0 && movement.y == 0)
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

        // A ACTIVER SI TU VEUT ENTENDRE LE BRUIT DE TES PAS JSP SI C'EST BIEN DONC JE COMMENTE
        /* 
        if (isMoving && Time.time >= nextTimeToPlay)
        {
            nextTimeToPlay = Time.time + 1f / playRate;
            AudioManager.GetComponent<AudioManager>().PlayPlayerWalking();
        }
        */

        rb.velocity = new Vector2(movement.x * playerStats.speed, movement.y * playerStats.speed);
    }

    public void HandleMoving()
    {
        float movX = 0;
        float movY = 0;

        if (Input.GetKey((KeyCode)inputs.getInputDico()["haut"]))
        {
            movY += 1;
        }

        if (Input.GetKey((KeyCode)inputs.getInputDico()["bas"]))
        {
            movY += -1;
        }

        if (Input.GetKey((KeyCode)inputs.getInputDico()["gauche"]))
        {
            movX += -1;
        }

        if (Input.GetKey((KeyCode)inputs.getInputDico()["droite"]))
        {
            movX += 1;
        }

        movement.x = movX;
        movement.y = movY;

    }
    private IEnumerator Dash()
    {
        if (this.playerStats.getCanDash())
        {

            this.playerStats.PlayDashAnim();

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
}
