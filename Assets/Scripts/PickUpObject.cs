using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Animator animator;
    public GameObject bonusMenu;
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {

            animator.SetTrigger("ChestOpening");
            StartCoroutine("waiter_not_that_waiter_just_waiter");

        }
    }
    IEnumerator waiter_not_that_waiter_just_waiter()
    {
        yield return new WaitForSeconds(1f);

        this.bonusMenu.SetActive(true);

        GameObject newObject = Instantiate(objectToSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);

    }
}