using UnityEngine;

public class Maintien_Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet est le joueur (par comparaison avec le tag)
        if (other.gameObject.tag == "Player")
        {
            // Faire de la plateforme le parent du joueur
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifier si l'objet quittant est le joueur
        if (other.gameObject.tag == "Player")
        {
            // Détacher le joueur de la plateforme
            other.transform.SetParent(null);
        }
    }
}
