using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAimWeapon : MonoBehaviour {

    public Camera cam;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer weaponRenderer;

    private Transform aimTransform;
    private Transform weaponTransform;
    private Transform gunEndPosition;

    private Animator WeaponAnimator;

    // SHOOTING 
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public float fireRate = 1f;
    private float nextTimeToFire = 0f;

    //Vaccum
    public bool isCharged;
    public float zombieCharged;
    public GameObject zombieHeadBulletPrefab;
    public float zombieHeadBulletForce = 10f;

    private ParticleSystem psVaccum;


    private void Awake()
    {
        
        this.isCharged = false;

        aimTransform = transform.Find("Aim");
        weaponTransform = aimTransform.Find("weapon");
        gunEndPosition = aimTransform.Find("GunEndPosition");

        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponRenderer = aimTransform.GetComponentInChildren<SpriteRenderer>();
        WeaponAnimator = aimTransform.GetComponentInChildren<Animator>();

        weaponTransform.Find("zombieHead").GetComponentInChildren<Renderer>().enabled = false;

        this.psVaccum = weaponTransform.Find("VaccumParticle").GetComponentInChildren<ParticleSystem>();
        this.psVaccum.Stop();

    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - aimTransform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        
        if (angle < 0)
        {
            if (angle < -90)
            {
                spriteRenderer.flipX = true;
                weaponRenderer.flipY = true;
                //weaponTransform.localPosition = new Vector3((float)1.25, (float)-2.02, 0);
            }
            else
            {
                spriteRenderer.flipX = false;
                weaponRenderer.flipY = false;
                //weaponTransform.localPosition = new Vector3((float)1.34, (float)1.68, 0);
            }
        }
        else
        {
            if (angle > 90)
            {
                spriteRenderer.flipX = true;
                weaponRenderer.flipY = true;
                //weaponTransform.localPosition = new Vector3((float)1.25, (float)-2.02, 0);
            }
            else
            {
                spriteRenderer.flipX = false;
                weaponRenderer.flipY = false;
                //weaponTransform.localPosition = new Vector3((float)1.34, (float)1.68, 0);
            }
        }
        
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire2") && !this.isCharged)
        {
            this.psVaccum.Play();
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            this.psVaccum.Stop();
        }


        if(Input.GetButton("Fire1"))
        {
            ShootCharge();
            weaponTransform.Find("zombieHead").GetComponentInChildren<Renderer>().enabled = false;

        }
    }

    public void setZombieCharged(float zombie)
    {
        this.zombieCharged = zombie;
        this.isCharged = true;
        weaponTransform.Find("zombieHead").GetComponentInChildren<Renderer>().enabled = true;
    }

    public float getZombieCharged()
    {
        return this.zombieCharged;
    }

    private void Shoot()
    {
        if (!this.isCharged)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet,0.2f);
        }
    }

    private void ShootCharge()
    {
        if (this.isCharged)
        {
            GameObject zombieHeadbullet = Instantiate(zombieHeadBulletPrefab, firePoint.position, firePoint.rotation);


            Rigidbody2D zombieHeadRb = zombieHeadbullet.GetComponent<Rigidbody2D>();

            zombieHeadRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            this.isCharged = false;
        }
    }
}
