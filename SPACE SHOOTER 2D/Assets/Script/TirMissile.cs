using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirMissile : MonoBehaviour
{
    public float moveSpeed = 6f;

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}