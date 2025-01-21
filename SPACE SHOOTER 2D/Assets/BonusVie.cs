using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVie : MonoBehaviour, IDamageable
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] float DespawnTime = 10f;

    void Start()
    {
        StartCoroutine(Despawn());
    }

    //////////////////////////////// ENDOMMAGEABLE + DEPLACEMENT //////////////////////////////////////////////////////////////////////////

    public void SetDamage(int damage, IDamageable attacker)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    //////////////////////////////// GAIN DE BONUS //////////////////////////////////////////////////////////////////////////

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControler.instance.SetDamage(-1, this);
            Destroy(gameObject);
        }
    }

    //////////////////////////////// ENDOMMAGEABLE + DEPLACEMENT //////////////////////////////////////////////////////////////////////////
    
    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnTime);
            Destroy(gameObject);
        }
    }
}
