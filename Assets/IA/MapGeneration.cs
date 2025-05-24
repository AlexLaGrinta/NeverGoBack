using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;

public class SkillBasedMapGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject platformPrefab;
    private int platformCount = 80;




    private float baseHorizontalRange = 5f;
    private float baseDepthRange = 5f;
    private float minSpacing = 2f;
    private float maxSpacing = 7f;
    private float verticalSpacing = 1f;

    private float skillLevel = 1f; 

    private float minX = 465.72f;
    private float maxX = 493.141f;
    private float fixedMinY = 16.437f;
    private float minZ = 406.84f;
    private float maxZ = 435.109f;

    private Vector3 lastPosition = Vector3.zero;
    private List<(int, int)> cubesCoord = new List<(int, int)>();

    private List<Vector3> allPos = new List<Vector3>();


    private Vector3[] pos;

    public Vector3[] PosGetter()
    {
        return pos;
    }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GeneratePlatforms();
        }
    }


    public void SendPlatToAll()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("PlaceAllPlat", RpcTarget.AllBuffered, allPos.ToArray());
        }
    }


    private bool checkCoord(int x, int z)
    {
        foreach ((int x2, int z2) in cubesCoord)
        {
            if (x == x2 && z == z2)
            {
                return true;
            }
        }
        return false;
    }

    [PunRPC]
    void PlaceAllPlat(Vector3[] lasPosiciones)
    {
        pos = lasPosiciones;

        foreach (var vect in lasPosiciones)
        {
            Instantiate(platformPrefab, vect, Quaternion.identity);
        }
        Vector3 coordCup = lasPosiciones[^1];
        coordCup.y += 0.1f;
        GameObject theCup = GameObject.FindGameObjectWithTag("Cup");
        theCup.transform.position = coordCup;
    }


    void GeneratePlatforms(Vector3? lastPos = null)
    {
        if (lastPos == null)
        {
            lastPosition = new Vector3(
                Random.Range(minX, maxX),
                fixedMinY,
                Random.Range(minZ, maxZ)
            );
        } else {
            lastPosition = (Vector3)lastPos;
        }

        for (int i = 0; i < platformCount; i++)
        {
            float horizontalRange = baseHorizontalRange * skillLevel;
            float depthRange = baseDepthRange * skillLevel;

            float newX = lastPosition.x;
            float newZ = lastPosition.z;
            do
            {
                newX = lastPosition.x + Random.Range(-horizontalRange, horizontalRange);
                newZ = lastPosition.z + Random.Range(-depthRange, depthRange);
            } while (Mathf.Abs(lastPosition.x - newX) < minSpacing || 
                    Mathf.Abs(lastPosition.x - newX) > maxSpacing ||
                    newX > maxX || newX < minX ||
                    Mathf.Abs(lastPosition.z - newZ) < minSpacing ||
                    Mathf.Abs(lastPosition.z - newZ) > maxSpacing || 
                    newZ > maxZ || newZ < minZ || 
                    checkCoord((int)newX, (int)newZ)
                    );

            float newY = lastPosition.y + verticalSpacing;
            Vector3 newPosition = new Vector3(newX, newY, newZ);

            allPos.Add(newPosition);

            //Instantiate(platformPrefab, newPosition, Quaternion.identity);
            cubesCoord.Add(((int)newPosition.x,(int)newPosition.z));
            lastPosition = newPosition;

            skillLevel = Mathf.Clamp(skillLevel + 0.05f, 1f, 10f);
        }
    }
}
