using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using TMPro;

public class zombie : MonoBehaviour
{
    // stats
    public int PV;
    public int Force;
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
    private TextMeshPro textMeshPro;


    public void Start()
    {
        this.isDead = false;

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();




        float randomNumber = Random.Range(0, 10);

        string Force = "4";// aleatoire.Next(1, 8); // Génère un entier compris entre 1 et 12

        //this.textMeshPro = GameObject.FindGameObjectWithTag("Zombie").GetComponent<TextMeshProUGUI>();

        this.textMeshPro = this.GetComponentInChildren<TextMeshPro>();

        /*Debug.Log("textMeshPro");
        Debug.Log("text = " + this.textMeshPro);*/

        //this.textMeshPro.SetText(Force);

        string String = randomNumber.ToString("R");

        Debug.Log("random" + String);

        this.textMeshPro.text = String;



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
                Destroy(other.gameObject);
                this.animator.SetBool("moving", false);
                target = null;
            }
            else if (other.gameObject.CompareTag("bullet"))
            {
                this.PV--;
            }
        }
    }

    public bool getIsDead()
    {
        return this.isDead;
    }

}
