using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using Photon.Pun;


public class MapLocation
{

    public int x { get; set; }

    public int z { get; set; }

    
    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x,z);
    }

    public static MapLocation operator +(MapLocation a, MapLocation b)
    => new MapLocation(a.x + b.x, a.z + b.z);


    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            return false;
        else
            return x == ((MapLocation)obj).x && z == ((MapLocation)obj).z;
    }

    public override int GetHashCode()
    {
        return 0;
    }

}





public class Maze : MonoBehaviourPunCallbacks
{

    private int width = 20;
    private int height = 20;

    private byte[,] map;

    private int scale = 6;


    private List<Vector3> allPosMaze = new List<Vector3>();

    public GameObject Cube;

    private List<Vector3> bannedCubes = new List<Vector3> { 
        new Vector3 (6,0,6),
        new Vector3 (12,0,6),
        new Vector3 (6,0,12),

        new Vector3 (102,0,108),
        new Vector3 (108,0,102),
        new Vector3 (108,0,108),
    };


    private List<MapLocation> directions = new List<MapLocation>() { 
        new MapLocation(1,0),
        new MapLocation(0,1),
        new MapLocation(-1,0),
        new MapLocation(0,-1),
    };

    public void Start()
    {
        
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InitializeMap();
            Generate();
            DrawMap();
            photonView.RPC("PlaceTheMaze", RpcTarget.AllBuffered,allPosMaze.ToArray());
        }
    }



    [PunRPC]
    public void PlaceTheMaze(Vector3[] lasPosicionesDelMaze)
    {
        foreach (var pos in lasPosicionesDelMaze)
        {
            GameObject wall = Instantiate(Cube, pos, Quaternion.identity);
            NavMeshObstacle NavWall = wall.AddComponent<NavMeshObstacle>();
            NavWall.carving = true;
        }
    }


    void InitializeMap()
    {
        map = new byte[width,height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1;
            }
        }
    }

    void Generate()
    {
        Generate(5, 5);
    }

    void Generate(int x, int z)
    {
        if (CountSquareNeighbours(x, z) >= 2) return;
        map[x, z] = 0;
        directions.Shuffle();

        Generate(x + directions[0].x, z + directions[0].z);
        Generate(x + directions[1].x, z + directions[1].z);
        Generate(x + directions[2].x, z + directions[2].z);
        Generate(x + directions[3].x, z + directions[3].z);

    }



    void DrawMap()
    {
        for (int z = 0; z < height;z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x,z] == 1)
                {
                    Vector3 pos = new Vector3(x*scale,0,z*scale);
                    bool found = false;
                    foreach (var cube in bannedCubes)
                    {
                        if (pos == cube) found = true;
                    }
                    if (found) continue;
                    //GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);


                    //GameObject wall = Instantiate(Cube, pos, Quaternion.identity);
                    //NavMeshObstacle NavWall = wall.AddComponent<NavMeshObstacle>();
                    //NavWall.carving = true;


                    //wall.transform.localScale = new Vector3(scale, scale, scale);
                    //wall.transform.position = pos;

                    allPosMaze.Add(pos);

                }
            }
        }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= height - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z+1] == 0) count++;
        if (map[x, z-1] == 0) count++;
        return count;

    }
}
