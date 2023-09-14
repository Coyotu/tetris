using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapController : MonoBehaviour
{
    [SerializeField] private ShapeSpawner _spawner;
    public int[,] matrix = new int[20, 10];
    private float startTime = 0.0f;
    private float currentTime = 0.0f;
    public int rowToDestroy = -1;
    public int rowThatHasBeenDestroyed = -1;
    private List<GameObject> squareObjects = new List<GameObject>();

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

        if (x1 < 10 && y1 < 20)
            matrix[y1, x1] = 1;

        if (x2 < 10 && y2 < 20)
            matrix[y2, x2] = 2;
        
        if (x3 < 10 && y3 < 20)
            matrix[y3, x3] = 3;
        
        if (x4 < 10 && y4 < 20)
            matrix[y4, x4] = 4;
        
        if (y1 < 19 && y2 < 19 && y3 < 19 && y4 < 19)
        {
            if (matrix[y1 + 1, x1] < 0 || matrix[y2 + 1, x2] < 0 || matrix[y3 + 1, x3] < 0 || matrix[y4 + 1, x4] < 0)
            {
                _spawner.SpawnObject();
                MakeMatrixElementsNegative();
                FindFullRow();
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

    private void FindFullRow()
    {
        int element;
        for (int i = 0; i < 20; i++)
        {
            element = 0;
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j] < 0)
                    element++;
            }

            if (element == 10)
                rowToDestroy = i;
        }
        Debug.Log(rowToDestroy);
    }

    public void markAsDestroyed(int row)
    {
        rowThatHasBeenDestroyed = rowToDestroy;
        int elements=0;
        for (int i = 0; i < 10; i++)
        {
            if (matrix[row, i] == 0)
                elements++;
        }
        
    }
    
    public bool EmptyRowExist()
    {
        bool isEmptyBetweenTheLines = false;
        int element;
        for (int i = 5; i < 20; i++)
        {
            element = 0;
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i - 1, j] != 0 && matrix[i,j]==0)
                    isEmptyBetweenTheLines = true;
                if (matrix[i, j] == 0)
                    element++;
            }

            if (element == 10 && isEmptyBetweenTheLines)
                return true;
        }

        return false;
    }

    private void Update()
    {
        if (EmptyRowExist() && rowToDestroy != -1)
        {
            Debug.Log("s-a incercat o manevra");
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                // Verificăm dacă numele obiectului conține "Square"
                if (obj.name.Contains("Square"))
                {
                    // Verificăm dacă obiectul nu există deja în vector
                    if (!squareObjects.Contains(obj))
                    {
                        squareObjects.Add(obj);
                    }
                }
            }
            Debug.Log(squareObjects.Count);

            for (int i=0;i<squareObjects.Count;i++)
            {
                if (rowToDestroy > (int)(-squareObjects[i].transform.position.y))
                {
                    Debug.Log("s-a incercat ceva");
                    GetBoxLowerOnMap _objScript = squareObjects[i].GetComponent<GetBoxLowerOnMap>();
                    _objScript.getLower();
                }
            }
            squareObjects.Clear();
            UpdateMapAfterDestroy(rowToDestroy);
            rowToDestroy = -1;
        }
    }

    private void UpdateMapAfterDestroy(int row)
    {
        for (int i = row; i > 6; i--)
        {
            for (int j = 0; j < 10; j++)
            {
                matrix[i, j] = matrix[i-1, j];
            }
        }
    }

    public void changeValue(int row, int column)
    {
        matrix[row, column] = 0;
        markAsDestroyed(row);
    }
}
