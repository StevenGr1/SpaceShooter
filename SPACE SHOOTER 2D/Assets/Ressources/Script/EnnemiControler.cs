using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class EnnemiControler : MonoBehaviour,IDamageable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject missileGo;
    [SerializeField] GameObject missilePosGo;

    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] float shootInterval = 2f;
    [SerializeField] float DespawnTime = 10f;

    private AudioSource audioSource;
    [SerializeField] float detectionZone = 3f;
    bool isPlayerInRange
    {
        get
        {
            return Vector3.Distance(transform.position, PlayerControler.instance.transform.position)<=detectionZone;
        }
    }

    void Start()
    {
        StartCoroutine(ShootRoutine());
        StartCoroutine(Despawn());

        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }


    //////////////////////////////// DEPLACEMENT + ENDOMMAGEABLE //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        if (isPlayerInRange)
        {
            audioSource.Play();
        }
    }
    public void SetDamage(int damage, IDamageable attacker)
    {
        throw new System.NotImplementedException();
    }

    //////////////////////////////// TIR AUTO //////////////////////////////////////////////////////////////////////////

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

    //////////////////////////////// DESPAWN ENNEMI //////////////////////////////////////////////////////////////////////////

    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnTime);
            Destroy(gameObject);
        }
    }
}