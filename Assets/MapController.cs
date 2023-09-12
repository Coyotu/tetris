using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapController : MonoBehaviour
{
    public int[,] matrix = new int[10, 20];

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                matrix[i,j] = 0;
            }
        }
    }
    
}
