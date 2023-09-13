using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _shape1;
    [SerializeField] private GameObject _shape2;
    [SerializeField] private GameObject _shape3;
    [SerializeField] private GameObject _shape4;
    [SerializeField] private GameObject _shape5;

    private void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        System.Random random = new System.Random();

        int x = random.Next(1, 6);
        switch (x)
        {
            case 1:
                Instantiate(_shape1);
                break;
            case 2:
                Instantiate(_shape2);
                break;
            case 3:
                Instantiate(_shape3);
                break;
            case 4:
                Instantiate(_shape4);
                break;
            case 5:
                Instantiate(_shape5);
                break;
        }
    }
}
