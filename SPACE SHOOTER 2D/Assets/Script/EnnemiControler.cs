using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiControler : MonoBehaviour,IDamageable
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

    //////////////////////////////// TIR AUTO //////////////////////////////////////////////////////////////////////////

    IEnumerator ShootRoutine()
    {
        Instantiate(missileGo, missilePosGo.transform.position, Quaternion.Euler(0, 0, -180));

        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            GameObject missileInst = Instantiate(missileGo, missilePosGo.transform.position, Quaternion.Euler(0, 0, -180));
            TirMissile tirMissile = missileInst.GetComponent<TirMissile>();
            tirMissile.Launcher = gameObject;
        }
    }

    //////////////////////////////// DEGAT DE TIR //////////////////////////////////////////////////////////////////////////
    
    public void SetDamage(int damage, IDamageable attacker)
    {
        Destroy(gameObject);
        PlayerControler.instance.SetDamage(1, null);
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