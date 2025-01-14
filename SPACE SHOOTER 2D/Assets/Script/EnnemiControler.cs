using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiControler : MonoBehaviour,Iinteractable
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    GameObject missileGo;
    [SerializeField]
    GameObject missilePosGo;

    [SerializeField]
    public float moveSpeed = 2f;
    [SerializeField]
    float shootInterval = 2f;

    float DespawnTime = 10f;

    void Start()
    {
        StartCoroutine(ShootRoutine());
        StartCoroutine(Despawn());
    }

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    IEnumerator ShootRoutine()
    {
        Instantiate(missileGo, missilePosGo.transform.position, Quaternion.Euler(0, 0, -180));

        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            Instantiate(missileGo, missilePosGo.transform.position, Quaternion.Euler(0, 0, -180));
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerControler.instance.SetDamage(1);

        Destroy(gameObject);
    }
}