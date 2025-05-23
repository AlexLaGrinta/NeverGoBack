using UnityEngine;

public class SpawnAtRandomHeight : MonoBehaviour
{
    [SerializeField] private GameObject prefabPersonne;
    [SerializeField] private Transform immeuble;
    [SerializeField] private float hauteurMin = 10f;
    [SerializeField] private float hauteurMax = 90f;

    public void SpawnPersonne()
    {
        float hauteur = Random.Range(hauteurMin, hauteurMax);
        Vector3 spawnPos = immeuble.position + new Vector3(0, hauteur, immeuble.localScale.z * 0.5f + 0.1f);
        Instantiate(prefabPersonne, spawnPos, Quaternion.identity);
    }
}