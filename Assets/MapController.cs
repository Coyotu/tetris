using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapController : MonoBehaviour
{
    [SerializeField] private ShapeSpawner _spawner;
    public int[,] matrix = new int[20, 10];
    private float startTime = 0.0f;
    private float currentTime = 0.0f;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                matrix[i, j] = 0;
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

    public bool MapUpdate(int x1, int x2, int x3, int x4, int y1, int y2, int y3, int y4)
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
        if (y1 < 19 && y2 < 19 && y3 < 19 && y4 < 19)
        {
            if (matrix[y1 + 1, x1] < 0 || matrix[y2 + 1, x2] < 0 || matrix[y3 + 1, x3] < 0 || matrix[y4 + 1, x4] < 0)
            {
                _spawner.SpawnObject();
                MakeMatrixElementsNegative();
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            MakeMatrixElementsNegative();
            _spawner.SpawnObject();
            return false;
        }
    }

    private void MakeMatrixElementsNegative()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j] > 0)
                    matrix[i, j] *= -1;
            }
        }
    }

    public bool isLeftAvailable(int x1, int x2, int x3, int x4, int y1, int y2, int y3, int y4)
    {
        if (matrix[y1, x1-1] < 0 || matrix[y2, x2-1] < 0 || matrix[y3, x3-1] < 0 || matrix[y4, x4-1] < 0)
        {
            return false;
        }

        return true;
    }

    public bool isRightAvailable(int x1, int x2, int x3, int x4, int y1, int y2, int y3, int y4)
    {
        if (matrix[y1, x1+1] < 0 || matrix[y2, x2+1] < 0 || matrix[y3, x3+1] < 0 || matrix[y4, x4+1] < 0)
        {
            return false;
        }

        return true;
    }
}
