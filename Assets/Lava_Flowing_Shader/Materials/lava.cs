using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    private Transform objTransform; // Remarque : "transform" est déjà réservé par Unity
    public float targetHeight = 76f; // Hauteur maximale souhaitée
    public float speed = 1f;         // Vitesse de montée

    void Start()
    {
        objTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // Si la position actuelle est inférieure à la cible, continue de monter
        if (objTransform.position.y < targetHeight)
        {
            // Calculer la nouvelle position en progressant sur l'axe Y
            objTransform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            // S'assurer de ne pas dépasser la hauteur cible
            objTransform.position = new Vector3(objTransform.position.x, targetHeight, objTransform.position.z);
        }
    }
}
