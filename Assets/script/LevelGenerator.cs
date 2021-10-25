using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap myMap;
    public GameObject showingMap;
    private GameObject[,] mapObject = new GameObject[30, 32];
    public GameObject[] prefab;
    private int[,] levelmap =
    {
      {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
      {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
      {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
      {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
      {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
      {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
      {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
      {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
      {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
      {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
      {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
      {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
      {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
      {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
      {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
     };
    void Start()
    {
        Destroy(showingMap);
        creatMap();
    }
    void Update()
    {

    }
    void creatMap()
    {
        for (int y = 0; y < levelmap.GetLength(0); y++)
        {
            for (int x = 0; x < levelmap.GetLength(1); x++)
            {
                float angle = angleCounter(y, x);
                if (levelmap[y, x] != 0)
                {
                    mapObject[y, x] = Instantiate(selectObject(y, x));
                    mapObject[y, x].transform.parent = transform;
                    mapObject[y, x].transform.position = new Vector3(-levelmap.GetLength(1) + x , levelmap.GetLength(0) - y - 1, 0);
                    mapObject[y, x].transform.Rotate(0, 0, angle, Space.Self);
                }
                // map to right
                if (levelmap[y, x] != 0)
                {
                    mapObject[y, mapObject.GetLength(1) - x - 1] = Instantiate(selectObject(y, x));
                    mapObject[y, mapObject.GetLength(1) - x - 1].transform.parent = transform;
                    mapObject[y, mapObject.GetLength(1) - x - 1].transform.position = new Vector3(levelmap.GetLength(1) - x -1, levelmap.GetLength(0) - y - 1, 0);
                    mapObject[y, mapObject.GetLength(1) - x - 1].transform.Rotate(0, 180, angle, Space.Self);
                }
                // map to down
                if (levelmap[y, x] != 0)
                {
                    mapObject[mapObject.GetLength(0) - y - 1, x] = Instantiate(selectObject(y, x));
                    mapObject[mapObject.GetLength(0) - y - 1, x].transform.parent = transform;
                    mapObject[mapObject.GetLength(0) - y - 1, x].transform.position = new Vector3(-levelmap.GetLength(1) + x , -levelmap.GetLength(0) + y + 1, 0);
                    mapObject[mapObject.GetLength(0) - y - 1, x].transform.Rotate(180, 0, angle, Space.Self);
                }
                //map to down right
                if (levelmap[y, x] != 0)
                {
                    mapObject[mapObject.GetLength(0) - y - 1, mapObject.GetLength(1) - x - 1] = Instantiate(selectObject(y, x));
                    mapObject[mapObject.GetLength(0) - y - 1, mapObject.GetLength(1) - x - 1].transform.parent = transform;
                    mapObject[mapObject.GetLength(0) - y - 1, mapObject.GetLength(1) - x - 1].transform.position = new Vector3(levelmap.GetLength(1) - x -1, -levelmap.GetLength(0) + y + 1, 0);
                    mapObject[mapObject.GetLength(0) - y - 1, mapObject.GetLength(1) - x - 1].transform.Rotate(180, 180, angle, Space.Self);
                }
            }

        }
    }

    GameObject selectObject(int y, int x)
    {
        switch (levelmap[y, x])
        {
            case 0: return null;
            case 1: return prefab[0];
            case 2: return prefab[1];
            case 3: return prefab[2];
            case 4: return prefab[3];
            case 5: return prefab[4];
            case 6: return prefab[5];
            case 7: return prefab[6];
        }
        return null;
    }

    float angleCounter(int y, int x)
    {
        if (levelmap[y, x] == 1)// outside corner
        {
            if (x == 0) //in left
            {
                if (y == 0) // top left
                { return 0; }
                else
                { return 90; }
            }
            else if (x == levelmap.GetLength(1)) // right,not in this levelmap
            {
                if (y == 0)
                { return -90.0f; }
                else
                { return 180; }
            }
            else // in the middle
            {
                if (levelmap[y - 1, x] == 2 && levelmap[y, x - 1] == 2) //connect 
                { return 180; }
                else if (levelmap[y + 1, x] == 2 && levelmap[y, x - 1] == 2)
                { return -90; }
                else if (levelmap[y - 1, x] == 2 && levelmap[y, x + 1] == 2)
                { return 90; }
                else
                { return 0; }
            }
        }

        else if (levelmap[y, x] == 2) //outside wall
        {
            if (y == 0)
            { return 90; }
            else if (x == 0)
            {
                if (levelmap[y, x + 1] == 1 || levelmap[y, x + 1] == 2)
                { return 90; }
                else
                { return 0; }
            }
            else if (x != 0 && y != 0)
            {
                if (levelmap[y, x - 1] == 1 || levelmap[y, x - 1] == 2 || levelmap[y, x + 1] == 1 || levelmap[y, x + 1] == 2)
                { return 90; }
                else
                { return 0; }
            }
            else
            { return 0; }
        }
        else if (levelmap[y, x] == 3)
        {
            if (x == levelmap.GetLength(1) - 1)
            {
                if (levelmap[y, x - 1] == 4 && levelmap[y - 1, x] == 4 && levelmap[y + 1, x] == 4)
                { return -90; }
                else if (levelmap[y - 1, x] == 4 && levelmap[y, x - 1] != 4) // right and up
                { return 90; }
                else
                { return -90; }
            }
            else // not in the side
            {
                if (levelmap[y - 1, x] == 4 && levelmap[y + 1, x] != 4 && levelmap[y, x - 1] == 4 && levelmap[y, x + 1] == 4)
                { return 90; }
                else if (levelmap[y - 1, x] != 4 && levelmap[y + 1, x] == 4 && levelmap[y, x - 1] == 4 && levelmap[y, x + 1] == 4)
                { return 0; }
                else
                {
                    if ((levelmap[y + 1, x] == 3 || levelmap[y + 1, x] == 4) && (levelmap[y, x + 1] == 3 || levelmap[y, x + 1] == 4))
                    { return 0; }
                    else if ((levelmap[y - 1, x] == 3 || levelmap[y - 1, x] == 4) && (levelmap[y, x + 1] == 3 || levelmap[y, x + 1] == 4))
                    { return 90; }
                    else if ((levelmap[y - 1, x] == 3 || levelmap[y - 1, x] == 4) && (levelmap[y, x - 1] == 3 || levelmap[y, x - 1] == 4))
                    { return 180; }
                    else
                    { return -90; }
                }
            }
        }
        else if (levelmap[y, x] == 4)
        {
            if (x == levelmap.GetLength(1) - 1) // right
            {
                if (levelmap[y, x - 1] == 4)
                { return 90; }
                else
                { return 0; }
            }
            else if (y == levelmap.GetLength(0) - 1)
            { return 0; }
            else
            {
                if ((levelmap[y, x - 1] != 4 && levelmap[y, x - 1] != 3) || (levelmap[y, x + 1] != 4 && levelmap[y, x + 1] != 3))
                {
                    if (levelmap[y - 1, x] == 0 && levelmap[y + 1, x] == 0)
                    { return 90; }
                    else
                    { return 0; }
                }
                else
                { return 90; }
            }
        }
        else
        { return 0; }
    }

}
