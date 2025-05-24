using UnityEngine;

using UnityEngine;

public class KnifeIdleMotion : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f; // Hauteur du mouvement
    [SerializeField] private float frequency = 1f;   // Vitesse du mouvement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPosition + new Vector3(0f, offsetY, 0f);
    }
}

