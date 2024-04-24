using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class zombie : MonoBehaviour
{
    private AudioManager AudioManager;
    
    // stats
    public float force;
    public int damage;
    public bool isDead;

    private GameHandler gameHandler;

    //Damages rate
    public float damageRate = 1f;
    private float nextTimeToDamage = 0f;

    // Vaccum
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

        this.AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();


        float newforce;
        if (force == 0f)
        {
            newforce = Random.Range(1, 10);
        }
        else
        {
            newforce = force;
        }
        UpdateScale(newforce);

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

            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void UpdateScale(float newforce)
    {
        this.force = newforce;
        this.textMeshPro = this.GetComponentInChildren<TextMeshPro>();
        string Stringforce = force.ToString("R");
        //Debug.Log("random = " + Stringforce);
        this.textMeshPro.text = Stringforce;
        float scaleforce = force * 0.1f;
        float scaleSpeed = force * 0.8f;
        //this.speed = scaleSpeed;
        //this.transform.localScale = new Vector2(scaleforce, scaleforce);
        //this.transform.localScale = new Vector2(this.transform.localScale.x * (1f + 0.1f * force), this.transform.localScale.y * (1f + 0.1f * force));
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (this.isDead == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!this.isPulled)
                {
                    StartCoroutine(applyDamage());
                }
                else
                {
                    if (!playerAimWeapon.isZombieCharged())
                    {
                        playerAimWeapon.setZombieCharged(force);
                        Destroy(this.gameObject);
                    }
                }
            }
            else if (other.gameObject.CompareTag("zombieHeadBullet"))
            {
                //add number
                float newForce1 = this.force;
                float newForce2 = playerAimWeapon.getZombieCharged();

                int actualModif = playerAimWeapon.getIndexMode();
                float newForce = 1;
                if (actualModif == 0)
                {
                    newForce = newForce1 - newForce2;
                }else if (actualModif == 1)
                {
                    newForce = newForce1 / newForce2;
                }
                else if (actualModif == 2)
                {
                    newForce = newForce1 * newForce2;
                }
                else if (actualModif == 3)
                {
                    newForce = newForce1 + newForce2;
                }

                UpdateScale(newForce);

                if(newForce == 0)
                {
                    kill();
                    gameHandler.addScore(10);
                }
                

            }
            else if (other.gameObject.CompareTag("bullet"))
            {

                Debug.Log("bullet");
                transform.position = Vector3.MoveTowards(transform.position, target.position, playerStats.getPullPower() * Time.deltaTime);
                StartCoroutine(SetIsPulledForDuration(0.2f));
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (this.isDead == false) {

            if (other.gameObject.CompareTag("Player"))
            {
                if (!this.isPulled)
                {
                    if (Time.time >= nextTimeToDamage)
                    {
                        nextTimeToDamage = Time.time + 1f / damageRate;
                        playerStats.takeDamage(10);
                    }
                }
                else
                {
                    playerAimWeapon.setZombieCharged(force);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void kill()
    {
        this.isDead = true;
        col.enabled = false;
        this.AudioManager.PlayZombieDeath();
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
            AudioManager.PlayDamage();
            playerStats.takeDamage(this.damage);
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

    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}
