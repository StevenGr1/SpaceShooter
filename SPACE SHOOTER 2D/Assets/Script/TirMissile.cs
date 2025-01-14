using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirMissile : MonoBehaviour, IDamageable
{
    public float moveSpeed = 6f;
    public int damage = 1;
    public GameObject Launcher;
    float DespawnTime = 10f;

    //////////////////////////////// MOUVEMENT //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        StartCoroutine(Despawn());
    }

    //////////////////////////////// COLLISION + DOMMAGE + DESPAWN TIR //////////////////////////////////////////////////////////////////////////

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Launcher) return;
        IDamageable iinteractable = collision.GetComponent<IDamageable>();
        if (iinteractable == null) return;
        iinteractable.SetDamage(1, this);
        Destroy(gameObject);
    }

    public void SetDamage(int damage, IDamageable attacker)
    {
        Destroy(gameObject);
    }

    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnTime);
            Destroy(gameObject);
        }
    }
}