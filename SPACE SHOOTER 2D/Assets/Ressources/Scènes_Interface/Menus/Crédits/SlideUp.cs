using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUp : MonoBehaviour
{
    private float speed = 40f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}