using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampas : MonoBehaviour
{
    BoxCollider2D colider;
    private void Start()
    {
        colider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().Muerte();
            StartCoroutine(Respawn_Corrutina());
        }
    }
    IEnumerator Respawn_Corrutina()
    {
        colider.enabled = false;
        yield return new WaitForSeconds(6);
        colider.enabled = true;

    }
}

