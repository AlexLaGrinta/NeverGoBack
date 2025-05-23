using System.Collections.Generic;
using UnityEngine;

public class WallSpawnerZone : MonoBehaviour
{
    [Header("Prefab et position de spawn")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform spawnPoint;  // Facultatif, sinon spawn sur la zone

    // Pour éviter de respawner plusieurs murs pour un même joueur
    private Dictionary<GameObject, GameObject> playerWalls = new Dictionary<GameObject, GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
            Quaternion rot = spawnPoint ? spawnPoint.rotation : transform.rotation;
            Instantiate(wallPrefab, pos, rot);

            Debug.Log("Un mur a été créé pour " + other.name);
        }
    }
}
