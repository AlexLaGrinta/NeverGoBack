using UnityEngine;

public class ArcherMob : MonoBehaviour
{
    [SerializeField] private GameObject prefabProjectile;
    [SerializeField] private Transform pointDeTir;
    [SerializeField] private float cadenceTir = 0.7f;    
    [SerializeField] private float vitesseProjectile = 10f;
    [SerializeField] private float rangeDeTir = 15f;
    
    private Transform cibleActuelle = null;
    private float prochainTir = 0f;

    void Update()
    {
        if (cibleActuelle != null)
        {
            float distance = Vector3.Distance(transform.position, cibleActuelle.position);
            if (distance <= rangeDeTir)
            {
                Vector3 direction = (cibleActuelle.position - pointDeTir.position).normalized;
                pointDeTir.forward = direction; 

                if (Time.time >= prochainTir)
                {
                    Tirer(direction);
                    prochainTir = Time.time + cadenceTir;
                }
            }
            else
            {
                cibleActuelle = null;
            }
        }
    }

    void Tirer(Vector3 direction)
    {
        GameObject proj = Instantiate(prefabProjectile, pointDeTir.position, Quaternion.LookRotation(direction));
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direction * vitesseProjectile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cibleActuelle = other.transform;
            prochainTir = Time.time; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform == cibleActuelle)
                cibleActuelle = null;
        }
    }
}
