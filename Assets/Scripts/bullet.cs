using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bullet : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
