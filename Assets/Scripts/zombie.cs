using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class zombie : MonoBehaviour
{
    // stats
    public int force;
    public bool isDead;

    private GameHandler gameHandler;

    // Vaccum
    public float aspiForce;
    private bool isPulled;


    // Render
    public Transform target;
    public float speed = 3f;

    public GameObject deathParticle;
    
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sprite;
    private Animator animator;

    public void Start()
    {
        this.isDead = false;
        this.isPulled = false;

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else if(!isDead)
        {
            Vector3 direction = (target.position - this.transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(angle < 90 && angle > -90)
            {
                sprite.flipX = false;
            }

            else
            {
                sprite.flipX = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        /*
        
        VERSION AVEC ZOMBIE QUI ON DES PV

        if(this.PV <= 0)
        {
            if(this.isDead == false)
            {
                this.isDead = true;
                col.enabled = false;
                animator.SetBool("isDead", this.isDead);
                Instantiate(deathParticle,transform.position,transform.rotation);

                this.gameHandler.addScore(15);

                Destroy(this.gameObject, 15);
            }
        }
        */
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (this.isDead == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if(!this.isPulled)
                {
                    Destroy(other.gameObject);
                    this.animator.SetBool("moving", false);
                    target = null;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAimWeapon>().setZombieCharged(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
            else if (other.gameObject.CompareTag("zombieHeadBullet"))
            {
                kill();
            }
            else if (other.gameObject.CompareTag("bullet"))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, aspiForce * Time.deltaTime);
                StartCoroutine(SetIsPulledForDuration(1f));
            }
        }
    }

    public void kill()
    {
        this.isDead = true;
        col.enabled = false;
        animator.SetBool("isDead", this.isDead);
        Instantiate(deathParticle, transform.position, transform.rotation);

        this.gameHandler.addScore(15);

        Destroy(this.gameObject, 15);
    }

    IEnumerator SetIsPulledForDuration(float duration)
    {
        this.isPulled = true;
        yield return new WaitForSeconds(duration);
        this.isPulled = false;
    }

    public bool getIsDead()
    {
        return this.isDead;
    }
    public bool getIsPulled()
    {
        return this.isPulled;
    }

}
