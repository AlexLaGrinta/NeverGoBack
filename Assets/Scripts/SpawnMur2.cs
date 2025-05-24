using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomWallSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> wallPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delay = 2f; // secondes entre chaque apparition de mur

    private Coroutine spawnCoroutine;

    void Start()
    {
        // Lance la boucle de spawn dès le début
        spawnCoroutine = StartCoroutine(SpawnWallsLoop());
    }

    private IEnumerator SpawnWallsLoop()
    {
        while (true) // Boucle infinie
        {
            SpawnRandomWall();
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnRandomWall()
    {
        if (wallPrefabs.Count == 0)
        {
            Debug.LogError("Aucun prefab de mur assigné !");
            return;
        }

        int index = Random.Range(0, wallPrefabs.Count);
        GameObject prefab = wallPrefabs[index];

        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        Quaternion rot = spawnPoint ? spawnPoint.rotation : transform.rotation;

        Instantiate(prefab, pos, rot);
    }
}