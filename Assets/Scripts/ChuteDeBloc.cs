using UnityEngine;
using System.Collections;

public class BlocPiege : MonoBehaviour
{
    [SerializeField] private Vector3 destination; // Position d'arrivée (en local ou global selon le besoin)
    [SerializeField] private float vitesse = 5f;
    [SerializeField] private float tempsArretBas = 2f;
    [SerializeField] private float tempsArretHaut = 1f;

    private Vector3 positionInitiale;

    private void Start()
    {
        positionInitiale = transform.position;
        StartCoroutine(BouclePiege());
    }

    private IEnumerator BouclePiege()
    {
        while (true)
        {
            // Descendre
            while (Vector3.Distance(transform.position, destination) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, vitesse * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(tempsArretBas);

            // Remonter
            while (Vector3.Distance(transform.position, positionInitiale) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, positionInitiale, vitesse * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(tempsArretHaut);
        }
    }
}
