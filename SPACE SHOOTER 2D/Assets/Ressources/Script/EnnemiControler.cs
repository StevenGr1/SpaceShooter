using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiControler : MonoBehaviour, IDamageable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject missileGo;
    [SerializeField] GameObject missilePosGo;

    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] float shootInterval = 2f;
    [SerializeField] float DespawnTime = 10f;

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public GameObject player;

    //////////////////////////////// START COROUTINE //////////////////////////////////////////////////////////////////////////
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");

        StartCoroutine(ShootRoutine());
        StartCoroutine(Despawn());
    }

    public void SetDamage(int damage, IDamageable attacker)
    {
        throw new System.NotImplementedException();
    }

    //////////////////////////////// MOUVEMENT + BRUIT MOTEUR(ZONE) //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

        if (player != null && audioSource2 != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= 5f)
            {
                audioSource2.Play();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    //////////////////////////////// TIR ET DESPAWN AUTO //////////////////////////////////////////////////////////////////////////

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            GameObject missileInst = Instantiate(missileGo, missilePosGo.transform.position, Quaternion.Euler(0, 0, -180));
            TirMissile tirMissile = missileInst.GetComponent<TirMissile>();
            tirMissile.Launcher = gameObject;
        }
    }

    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnTime);
            Destroy(gameObject);
        }
    }

    //////////////////////////////// COLLISION //////////////////////////////////////////////////////////////////////////

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Destruction());
        }
    }

    IEnumerator Destruction()
    {
        if (PlaylistB.instance != null)
        {
            AudioClip clip = PlaylistB.instance.GetAudioClip(0);
            if (clip != null)
            {
                if (audioSource != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            }
        }
        IHM.instance.SetDamage(1, this);
        IHM.instance.SetPoint(1);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        if (audioSource != null && audioSource.clip != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        Destroy(gameObject);
    }
}