using UnityEngine;

public class ArcherMob : MonoBehaviour
{
    public GameObject prefabProjectile;
    public Transform pointDeTir;
    public float cadenceTir = 0.7f;    // Temps entre deux tirs (en secondes)
    public float vitesseProjectile = 10f;
    public float rangeDeTir = 15f;
    
    private Transform cibleActuelle = null;
    private float prochainTir = 0f;

    void Update()
    {
        if (cibleActuelle != null)
        {
            float distance = Vector3.Distance(transform.position, cibleActuelle.position);
            if (distance <= rangeDeTir)
            {
                // Regarder vers le joueur (facultatif)
                Vector3 direction = (cibleActuelle.position - pointDeTir.position).normalized;
                pointDeTir.forward = direction; // Optionnel : vise le joueur

                // Tirer si le cooldown est ok
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

    // Détection entrée dans la zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cibleActuelle = other.transform;
            prochainTir = Time.time; // Peut tirer tout de suite
        }
    }

    // Détection sortie de la zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform == cibleActuelle)
                cibleActuelle = null;
        }
    }
}
