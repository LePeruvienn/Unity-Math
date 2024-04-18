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
    public float force;
    public int damage;
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
    private TextMeshPro textMeshPro;


    //Player
    private PlayerStats playerStats;

    //Player aim
    private PlayerAimWeapon playerAimWeapon;

    public void Start()
    {
        
        GetTarget();

        this.isDead = false;
        this.isPulled = false;

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        float randomNumber = Random.Range(1, 10);
        force = randomNumber * 2;
        string String = randomNumber.ToString("R");

        this.textMeshPro = this.GetComponentInChildren<TextMeshPro>();
        Debug.Log("random = " + String);
        this.textMeshPro.text = String;

        float realforce = force * 0.1f;
        this.transform.localScale = new Vector2(realforce, realforce);



        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerAimWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAimWeapon>();
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
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
        if (this.isDead == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if(!this.isPulled)
                {
                    StartCoroutine(applyDamage());
                }
                else
                {
                    playerAimWeapon.setZombieCharged(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
            else if (other.gameObject.CompareTag("zombieHeadBullet"))
            {
                //add number
                Instantiate(playerAimWeapon.get)
                //kill();
            }
            else if (other.gameObject.CompareTag("bullet"))
            {

                Debug.Log("bullet");
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

    IEnumerator applyDamage()
    {
        while(col.IsTouching(target.GetComponent<Collider2D>()))
        {
            playerStats.takeDamage(10);
            yield return new WaitForSeconds(1f);
        }
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
