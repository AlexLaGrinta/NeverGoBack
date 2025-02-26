using UnityEngine;
using System.Collections.Generic;

public class SkillBasedMapGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Le prefab pour les plateformes
    private int platformCount = 60; // Nombre total de plateformes à générer
    private float baseHorizontalRange = 0f; // Portée horizontale de base
    private float baseDepthRange = 0f; // Portée de variation sur l'axe Z (profondeur)

    private Vector3 lastPosition = Vector3.zero; // Position de la dernière plateforme placée
    private float skillLevel = 1f; // Niveau de compétence du joueur (au départ, facile)

    private float minX = 465.72f;
    private float maxX = 493.141f;
    private float fixedMinY = 16.437f; 
    private float minZ = 406.84f;
    private float maxZ = 435.109f;

    
    private List<(int,int)> cubesCoord = new List<(int, int)> ();

    void Start()
    {
        GeneratePlatforms();
    }


    private bool checkCoord(int x, int z)
    {

        foreach ((int x2,int z2) in cubesCoord)
        {
            if (x == x2 && z == z2)
            {
              return true;
            }
        }
        return false;
    }
    void GeneratePlatforms()
    {
        baseHorizontalRange = 5f;
        baseDepthRange = 5f;
        // Définir la première position aléatoire dans la zone (à l'intérieur du carré)
        lastPosition = new Vector3(
        Random.Range(minX, maxX),
        fixedMinY,
        Random.Range(minZ, maxZ)
        );

        for (int i = 0; i < platformCount; i++)
        {
            // Ajuster l'espacement vertical (Y) en fonction du skillLevel
            float verticalSpacing = 1f;

            // Ajuster la portée horizontale (X) et profondeur (Z) en fonction du skillLevel
            float horizontalRange = baseHorizontalRange * skillLevel;
            float depthRange = baseDepthRange * skillLevel;

            float newX = lastPosition.x;
            float newZ = lastPosition.z;
            do
            {
                newX = lastPosition.x + Random.Range(-horizontalRange, horizontalRange);
                newZ = lastPosition.z + Random.Range(-depthRange, depthRange);
            } while (Mathf.Abs(lastPosition.x - newX) < 2 || Mathf.Abs(lastPosition.x - newX) > 7 || newX > maxX || newX < minX || Mathf.Abs(lastPosition.z - newZ) < 2 || Mathf.Abs(lastPosition.z - newZ) > 7 || newZ > maxZ || newZ < minZ || checkCoord((int)newX, (int)newZ));



            // Calcul d'un nouveau Z en restant également dans les limites du carré
            float newY = lastPosition.y + verticalSpacing; //+ Random.Range(-depthRange, depthRange);

            // Position finale calculée
            Vector3 newPosition = new Vector3(newX, newY, newZ);

            // Instancier une nouvelle plateforme
            Instantiate(platformPrefab, newPosition, Quaternion.identity);

            cubesCoord.Add(((int)newPosition.x,(int)newPosition.z));


            // Enregistrer cette position comme la dernière utilisée
            lastPosition = newPosition;

            // Augmenter progressivement le niveau de difficulté
            skillLevel = Mathf.Clamp(skillLevel + 0.05f, 1f, 10f); // Progression douce et plafonnée
        }
    }
}