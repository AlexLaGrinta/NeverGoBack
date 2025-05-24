using UnityEngine;

public class PlateformeReapparition : MonoBehaviour
{
    [SerializeField] private float delaiDisparition = 0.1f;
    [SerializeField] private float delaiReapparition = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(MasquerPlateforme), delaiDisparition);
        }
    }

    void MasquerPlateforme()
    {
        gameObject.SetActive(false);
        Invoke(nameof(ReapparaitrePlateforme), delaiReapparition);
    }

    void ReapparaitrePlateforme()
    {
        gameObject.SetActive(true);
    }
}