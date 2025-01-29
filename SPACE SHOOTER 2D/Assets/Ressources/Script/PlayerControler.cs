using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject missileGo;
    [SerializeField] GameObject missilePosGo;
    [SerializeField] float speed = 5f;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;
    [SerializeField] float minY = -5f;
    [SerializeField] float maxY = 5f;

    Vector3 inputValue;

    //////////////////////////////// AUDIO + TOUCHES //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        inputValue.x = Input.GetAxis("Horizontal");
        inputValue.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 1;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject missileInstance = Instantiate(missileGo, missilePosGo.transform.position, Quaternion.identity);
        TirMissile tirMissile = missileInstance.GetComponent<TirMissile>();
        tirMissile.Launcher = gameObject;
    }

    void FixedUpdate()
    {
        Vector3 nextPos = transform.position + (inputValue * speed * Time.fixedDeltaTime);
        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        rb.MovePosition(nextPos);
    }

    //////////////////////////////// LIMITE DE DÉPLACEMENT //////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }
}