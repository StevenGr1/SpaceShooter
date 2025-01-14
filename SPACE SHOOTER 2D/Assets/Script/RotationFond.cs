using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFond : MonoBehaviour
{
    // La vitesse de rotation (en degrés par seconde)
    public float rotationSpeed = -0.5f;

    // Update is called once per frame
    void Update()
    {
        // Effectuer une rotation autour de l'axe Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}