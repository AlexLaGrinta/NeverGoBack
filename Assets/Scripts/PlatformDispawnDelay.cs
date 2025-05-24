using UnityEngine;
using System.Collections;

public class DisappearingPlatformDelay : MonoBehaviour
{
    [SerializeField] private float intervalleSecondes = 3f; // Temps entre apparition et disparition
    [SerializeField] private float startDelay = 0f; // Décalage avant la première disparition

    private MeshRenderer meshRenderer;
    private Collider platformCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        platformCollider = GetComponent<Collider>();

        // Lancer la routine d’apparition/disparition avec un décalage initial
        StartCoroutine(AppearDisappearRoutine());
    }

    IEnumerator AppearDisappearRoutine()
    {
        // Appliquer le décalage initial
        yield return new WaitForSeconds(startDelay);

        while (true) // Boucle infinie
        {
            yield return new WaitForSeconds(intervalleSecondes);
            SetPlatformState(false); // Disparition

            yield return new WaitForSeconds(intervalleSecondes);
            SetPlatformState(true); // Réapparition
        }
    }

    void SetPlatformState(bool isActive)
    {
        meshRenderer.enabled = isActive; // Affiche ou cache la plateforme
        platformCollider.enabled = isActive; // Active/désactive la collision
    }
}
