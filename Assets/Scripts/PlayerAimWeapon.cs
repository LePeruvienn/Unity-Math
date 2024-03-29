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

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        weaponTransform = aimTransform.Find("weapon");
        gunEndPosition = aimTransform.Find("GunEndPosition");

        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponRenderer = aimTransform.GetComponentInChildren<SpriteRenderer>();
        WeaponAnimator = aimTransform.GetComponentInChildren<Animator>();

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
        if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
