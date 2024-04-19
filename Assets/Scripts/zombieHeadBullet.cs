using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHeadBullet : MonoBehaviour
{
    public GameObject ZombiePrefab;

    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("bullet"))
        {
            if (!other.gameObject.CompareTag("Zombie"))
            {
                float getZombieCharged = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAimWeapon>().getZombieCharged();
                GameObject Zombie = Object.Instantiate(ZombiePrefab, this.gameObject.transform.position, Quaternion.identity);
                Zombie.GetComponent<zombie>().UpdateScale(getZombieCharged);
                Debug.Log("getZombieCharged = " + getZombieCharged);
            }
            Destroy(this.gameObject);
        }
    }
}
