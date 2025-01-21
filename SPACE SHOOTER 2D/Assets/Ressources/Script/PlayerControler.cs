using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour, IDamageable
{
    public static PlayerControler instance;
    public int maxLife = 5;
    public int currentLife = 5;
    public int currentPoint = 0;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject missileGo;
    [SerializeField] GameObject missilePosGo;
    [SerializeField] float speed = 5f;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;
    [SerializeField] float minY = -5f;
    [SerializeField] float maxY = 5f;

    Vector3 inputValue;

    private AudioSource audioSource;

    //////////////////////////////// INSTANCE //////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        instance = this;
        currentLife = maxLife;
    }

    //////////////////////////////// POSITION DU JOUEUR + DETECTION + SONS + TOUCHES //////////////////////////////////////////////////////////////////////////

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        inputValue.x = Input.GetAxis("Horizontal");
        inputValue.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.timeScale == 0)
            {

            }
            else
            {
                Time.timeScale = 1;
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 nextPos = transform.position + (inputValue * speed * Time.fixedDeltaTime);
        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        rb.MovePosition(nextPos);
    }

    //////////////////////////////// TIR DU JOUEUR //////////////////////////////////////////////////////////////////////////

    void Shoot()
    {
        GameObject missileInstance = Instantiate(missileGo, missilePosGo.transform.position, Quaternion.identity);
        TirMissile tirMissile = missileInstance.GetComponent<TirMissile>();
        tirMissile.Launcher = gameObject;
    }

    //////////////////////////////// SYSTEME DE PERTE DE VIE + EXPLOSIONS  //////////////////////////////////////////////////////////////////////////

    public void SetDamage(int damage, IDamageable attacker)
    {
        currentLife -= damage;
        IHM.instance.UpdatePlayerLife();

        if (currentLife <= 0)
        {
            StartCoroutine(DelaisDepop(true));
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CircleCollider2D>())
        {
            return;
        }
        if (collision.CompareTag("Ennemi"))
        {
            StartCoroutine(DelaisDepop(false, collision));
        }
    }

    //////////////////////////////// SYSTEME DE GAIN DE KILL //////////////////////////////////////////////////////////////////////////

    public void SetPoint(int point)
    {
        currentPoint += point;
        IHM.instance.UpdatePlayerPoint();
    }

    //////////////////////////////// AFFICHAGE ZONE DE DÉPLACEMENT //////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }

    //////////////////////////////// ATTENTE /////////////////////////////////////////////////////////////////////////////////////////

    IEnumerator DelaisDepop(bool isPlayer)
    {
        if (isPlayer)
        {
            currentLife = 0;
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }

    IEnumerator DelaisDepop(bool isPlayer, Collider2D collision)
    {
        if (collision.GetComponent<CircleCollider2D>() != null)
        {
            yield break;
        }
        if (!isPlayer && collision.CompareTag("Ennemi"))
        {
            PlayerControler.instance.SetDamage(1, this);
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
            collision.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            Destroy(collision.gameObject);
        }
    }
}