using UnityEngine;

public class Pendule : MonoBehaviour
{
    private float speed = 3f;    // Vitesse de balancement
    private float angle = 90f;   // Angle maximal
    private float delay;
    void Update()
    {
        delay = transform.position.z;
        float rotationX = Mathf.Sin(Time.time * speed + Mathf.Abs(delay)) * angle;
        transform.rotation = Quaternion.Euler(0, 0, rotationX);
    }
}