using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class GetBoxLowerOnMap : MonoBehaviour
{
    private MapController _map;
    private ShapeControl _shape;
    private bool _shouldGoLower = false;

    private void Start()
    {
        _map = FindObjectOfType<MapController>().GetComponent<MapController>();
        _shape = GetComponentInParent<ShapeControl>();
    }


    public void getLower()
    {
        switch (gameObject.name)
        {
            case "Square":
                _shape.y1--;
                break;
            case "Square (1)":
                _shape.y2--;
                break;
            case "Square (2)":
                _shape.y3--;
                break;
            case "Square (3)":
                _shape.y4--;
                break;
        }
    }
}