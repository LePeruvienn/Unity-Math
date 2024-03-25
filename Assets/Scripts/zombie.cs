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
    public int PV;
    public bool isDead;

    // Game object avec léquel il intéragit
    private GameHandler gameHandler;

    //NavMesh (pour l'IA)
    //NavMeshAgent agent;

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

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        /*
        this.agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        */

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

            //agent.SetDestination(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

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
                //Destroy(other.gameObject);
                //this.animator.SetBool("moving", false);
                //target = null;
            }
            else if (other.gameObject.CompareTag("bullet"))
            {
                //this.PV--;
                transform.position = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
            }
        }
    }

    public bool getIsDead()
    {
        return this.isDead;
    }

}
