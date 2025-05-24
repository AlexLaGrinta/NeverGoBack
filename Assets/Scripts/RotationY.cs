using UnityEngine;

public class RotateY : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f , 0f);
    }
}