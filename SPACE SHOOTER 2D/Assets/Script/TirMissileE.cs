using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirMissileE : MonoBehaviour
{
    public float moveSpeed = 6f;
    float DespawnInterval = 3f;

    void Start()
    {
        StartCoroutine(Despawn());
    }

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    IEnumerator Despawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(DespawnInterval);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject interfaceGO = GameObject.Find("Interface");

            if (interfaceGO != null)
            {
                Transform canvasTransform = interfaceGO.transform.Find("Canvas");

                if (canvasTransform != null)
                {
                    Transform barreDeVieTransform = canvasTransform.Find("Barre de Vie");

                    if (barreDeVieTransform != null)
                    {
                        Transform nombreDeVieTransform = barreDeVieTransform.Find("Nombre de Vie");

                        if (nombreDeVieTransform != null)
                        {
                            Text vieText = nombreDeVieTransform.GetComponent<Text>();

                            if (vieText != null)
                            {
                                int vieRestante = int.Parse(vieText.text);
                                vieRestante -= 1;
                                vieText.text = vieRestante.ToString();

                                if (vieRestante <= 0)
                                {
                                    Time.timeScale = 0;
                                    Destroy(collision.gameObject);
                                }
                            }
                        }
                    }
                }
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}