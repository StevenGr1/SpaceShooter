using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerControler : MonoBehaviour, IDamageable
{
    public static PlayerControler instance;
    public int maxLife = 5;
    public int currentLife = 5;
    public int currentPoint = 0;

    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    GameObject missileGo;
    [SerializeField]
    GameObject missilePosGo;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float minX = -5f;
    [SerializeField]
    float maxX = 5f;
    [SerializeField]
    float minY = -5f;
    [SerializeField]
    float maxY = 5f;

    Vector3 inputValue;


    void Awake()
    {
        instance = this;
        currentLife = maxLife;
    }

    //////////////////////////////// POSITION DU JOUEUR + TIR DU JOUEUR //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        inputValue.x = Input.GetAxis("Horizontal");
        inputValue.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector3 nextPos = transform.position + (inputValue * speed * Time.fixedDeltaTime);

        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);

        rb.MovePosition(nextPos);
    }

    void Shoot()
    {
        GameObject missileInstance = Instantiate(missileGo, missilePosGo.transform.position, Quaternion.identity);
        TirMissile tirMissile = missileInstance.GetComponent<TirMissile>(); // Agit sur l'instance, pas le prefab
        tirMissile.Launcher = gameObject;
    }


    //////////////////////////////// PERTE DE VIE //////////////////////////////////////////////////////////////////////////

    public void SetDamage(int damage, IDamageable attacker)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            currentLife = 0;
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
        IHM.instance.UpdatePlayerLife();
    }

    //////////////////////////////// GAIN DE KILL //////////////////////////////////////////////////////////////////////////

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile")) return;
        IDamageable iinteractable = collision.GetComponent<IDamageable>();
        if (iinteractable == null) return;
        iinteractable.SetDamage(1, this);
        SetDamage(1, null);
    }

    public void SetPoint(int point)
    {
        currentPoint += point;
        Debug.Log("A fait +1 kill");
        IHM.instance.UpdatePlayerPoint();
    }

    //////////////////////////////// AFFICHAGE ZONE DE DEPLACEMENT //////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }
}
