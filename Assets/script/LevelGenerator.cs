using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap myMap;
    public GameObject showingMap;
    public Sprite[] wallblock;
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
        myMap.transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
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
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = selectSprite(y, x);
                float angle = angleCounter(y, x);
                tile.transform = rotating(angle);
                myMap.SetTile(new Vector3Int(-levelmap.GetLength(1) + x  , levelmap.GetLength(0) - y - 1, 0), tile);
                tile.transform = mirrorToRight(angle);
                myMap.SetTile(new Vector3Int(levelmap.GetLength(1) - x -1 , levelmap.GetLength(0) - y - 1, 0), tile);
                tile.transform = mirrorToDown(angle);
                myMap.SetTile(new Vector3Int(-levelmap.GetLength(1) + x  , -levelmap.GetLength(0) + y + 1, 0), tile);
                tile.transform = mirrorToRightDown(angle);
                myMap.SetTile(new Vector3Int(levelmap.GetLength(1) - x -1 , -levelmap.GetLength(0) + y + 1, 0), tile);
            }

        }
    }

    Sprite selectSprite(int y, int x)
    {
        switch (levelmap[y, x])
        {
            case 1: return wallblock[0];
            case 2: return wallblock[1];
            case 3: return wallblock[2];
            case 4: return wallblock[3];
            case 5: return wallblock[4];
            //case 6: return wallblock[5];
            case 7: return wallblock[6];
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

    Matrix4x4 mirrorToRight(float D)
    {
        Quaternion an = Quaternion.Euler(0, 180, D);
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, an, Vector3.one);
        return m;
    }
    Matrix4x4 mirrorToDown(float D)
    {
        Quaternion an = Quaternion.Euler(180, 0, D);
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, an, Vector3.one);
        return m;
    }
    Matrix4x4 mirrorToRightDown(float D)
    {
        Quaternion an = Quaternion.Euler(180, 180, D);
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, an, Vector3.one);
        return m;
    }

    Matrix4x4 rotating(float D)
    {
        Quaternion an = Quaternion.Euler(0, 0, D);
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, an, Vector3.one);
        return m;
    }
}
