using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetBoxLowerOnMap : MonoBehaviour
{ 
    private MapController _map;
    private bool _shouldGoLower=false;

    private void Start()
    {
        _map = FindObjectOfType<MapController>().GetComponent<MapController>();
    }

    private void Update()
    {
        
    }
}
