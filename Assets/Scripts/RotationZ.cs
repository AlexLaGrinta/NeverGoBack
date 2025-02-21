using UnityEngine;

public class RotateZ : MonoBehaviour
{
    // Vitesse de rotation dans la direction Z (modifiable depuis l'inspecteur)
    public float rotationSpeed = 50f;

    void Update()
    {
        // Applique une rotation sur l'axe Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
