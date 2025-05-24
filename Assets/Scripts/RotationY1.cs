using UnityEngine;

public class RotateOnY : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f; // en degrés par seconde

    void Update()
    {
        // On fait tourner l'objet autour de l’axe Y, selon la vitesse paramétrée
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}