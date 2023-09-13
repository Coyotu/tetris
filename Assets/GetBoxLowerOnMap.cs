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
        if (_map.rowToDestroy < transform.position.y && _map.EmptyRowExist())
        {
            _shouldGoLower = true;
        }

        if (_shouldGoLower)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            _shouldGoLower = false;
        }
    }
}
