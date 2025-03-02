using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f , 0f);
    }
}