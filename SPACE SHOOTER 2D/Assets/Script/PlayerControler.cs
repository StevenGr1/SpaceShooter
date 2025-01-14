using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
    public int maxLife = 5;
    public int currentLife = 5;

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

    public void SetDamage(int Damage)
    {
        currentLife -= Damage;
        if (currentLife <= 0)
        {
            currentLife = 0;
            Debug.Log("Game Over");
        }
        IHM.instance.UpdatePlayerLife();
    }

    void Shoot()
    {
        Instantiate(missileGo, missilePosGo.transform.position, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }
}
