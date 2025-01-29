using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFond : MonoBehaviour
{
    private float rotationSpeed = -0.7f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}