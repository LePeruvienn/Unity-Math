using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHeadBullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
