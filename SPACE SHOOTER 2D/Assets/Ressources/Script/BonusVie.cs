using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVie : MonoBehaviour, IDamageable
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] float DespawnTime = 10f;
    [SerializeField] float BoomTime = 1f;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(Despawn());
        AudioClip clip = PlaylistB.instance.GetAudioClip(5);
        audioSource.clip = clip;
    }

    public void SetDamage(int damage, IDamageable attacker)
    {
        throw new System.NotImplementedException();
    }

    //////////////////////////////// MOUVEMENT //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    //////////////////////////////// GAIN DE BONUS //////////////////////////////////////////////////////////////////////////

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IHM.instance.SetDamage(-1, this);
            if (audioSource != null)
            {
                audioSource.Play();
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                StartCoroutine(Boom());
            }
        }
    }

    /////////////////////////////////////// DESPAWN //////////////////////////////////////////////////////////////////////////
    
    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnTime);
            Destroy(gameObject);
        }
    }

    IEnumerator Boom()
    {
        while (true)
        {
            yield return new WaitForSeconds(BoomTime);
            Destroy(gameObject);
        }
    }
}
