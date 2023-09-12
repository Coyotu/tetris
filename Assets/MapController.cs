using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapController : MonoBehaviour
{
    public int[,] matrix = new int[20, 10];
    public float delay = 1;
    private float startTime = 0.0f;
    private float currentTime = 0.0f;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                matrix[i,j] = 0;
            }
        }
    }
    

    public void Instantiate(int shapeIndex)
    {
        switch (shapeIndex)
        {
            case 0:
                matrix[0, 3] = 1;
                matrix[0, 4] = 2;
                matrix[0, 5] = 3;
                matrix[0, 6] = 4;
                break;
            case 1:
                matrix[0, 4] = 1;
                matrix[1, 4] = 2;
                matrix[1, 5] = 3;
                matrix[2, 5] = 4;
                break;
            case 2:
                matrix[0, 4] = 1;
                matrix[1, 4] = 2;
                matrix[2, 4] = 3;
                matrix[2, 5] = 4;
                break;
            case 3:
                matrix[0, 3] = 1;
                matrix[0, 4] = 2;
                matrix[0, 5] = 3;
                matrix[1, 4] = 4;
                break;
            case 4:
                matrix[0, 4] = 1;
                matrix[0, 5] = 2;
                matrix[1, 4] = 3;
                matrix[1, 5] = 4;
                break;
        }
    }
    void Update()
    {

    }

    public void MapUpdate(int x1,int x2,int x3, int x4,int y1,int y2,int y3, int y4)
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j] > 0)
                    matrix[i, j] = 0;
            }
        }
        matrix[y1, x1] = 1;
        matrix[y2, x2] = 2;
        matrix[y3, x3] = 3;
        matrix[y4, x4] = 4;
    }

    public int returnPosX(int var)
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j] == var)
                {
                    Debug.Log(var+ " " +j);
                    return j;
                }
            }
        }

        return 0;
    }
    public int returnPosY(int var)
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j] == var)
                {
                    Debug.Log(var+ " " +i);
                    return i;
                }
            }
        }

        return 0;
    }
}
