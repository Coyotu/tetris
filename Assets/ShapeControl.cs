using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class ShapeControl : MonoBehaviour
{
    private MapController _map;
    [SerializeField] private GameObject _box1;
    [SerializeField] private GameObject _box2;
    [SerializeField] private GameObject _box3;
    [SerializeField] private GameObject _box4;
    public int shapeIndex = 0;
    public int shapeRotation = 0;
    public float delay = 1;
    private float startTime = 0.0f;
    private float currentTime = 0.0f;
    private bool _canMove = true;
    private bool _speedMove = true;
    float x1 = 0.5f, x2 = 0.5f, x3 = 0.5f, x4 = 0.5f;
    public float y1 = -0.5f;
    public float y2 = -0.5f;
    public float y3 = -0.5f;
    public float y4 = -0.5f;
    private bool _shouldGoLower;


    void Start()
    {
        _map = FindObjectOfType<MapController>().GetComponent<MapController>();
        switch (shapeIndex)
        {
            case 0:
                _map.Instantiate(0);
                x1 = 3.5f;
                x2 = 4.5f;
                x3 = 5.5f;
                x4 = 6.5f;
                y1 = -0.5f;
                y2 = -0.5f;
                y3 = -0.5f;
                y4 = -0.5f;
                break;
            case 1:
                _map.Instantiate(1);
                x1 = 4.5f;
                x2 = 4.5f;
                x3 = 5.5f;
                x4 = 5.5f;
                y1 = -0.5f;
                y2 = -1.5f;
                y3 = -1.5f;
                y4 = -2.5f;
                break;
            case 2:
                _map.Instantiate(2);
                x1 = 4.5f;
                x2 = 4.5f;
                x3 = 4.5f;
                x4 = 5.5f;
                y1 = -0.5f;
                y2 = -1.5f;
                y3 = -2.5f;
                y4 = -2.5f;
                break;
            case 3:
                _map.Instantiate(3);
                x1 = 3.5f;
                x2 = 4.5f;
                x3 = 5.5f;
                x4 = 4.5f;
                y1 = -0.5f;
                y2 = -0.5f;
                y3 = -0.5f;
                y4 = -1.5f;
                break;
            case 4:
                _map.Instantiate(4);
                x1 = 4.5f;
                x2 = 5.5f;
                x3 = 4.5f;
                x4 = 5.5f;
                y1 = -0.5f;
                y2 = -0.5f;
                y3 = -1.5f;
                y4 = -1.5f;
                break;
        }
    }

    void Update()
    {
        if (_shouldGoLower)
        {
            //y1--;
            //y2--;
            //y3--;
            //y4--;
            Debug.Log("nununu");

            _shouldGoLower = false;
        }
        if(_box1!=null)
        _box1.transform.position = new Vector3(x1, y1, 0);
        if(_box2!=null)
        _box2.transform.position = new Vector3(x2, y2, 0);
        if(_box3!=null)
        _box3.transform.position = new Vector3(x3, y3, 0);
        if(_box4!=null)
        _box4.transform.position = new Vector3(x4, y4, 0);
        if (Input.GetKeyDown(KeyCode.UpArrow) && _canMove)
        {
            if(shapeIndex == 0 && x2>2 && x2<8)
                RotateShape(shapeRotation);
            else if(x2>1 && x2<9)
                RotateShape(shapeRotation);
        }
        

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _speedMove = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && _canMove)
        {
            if (x1 > 1 && x2 > 1 && x3 > 1 && x4 > 1)
            {
                if (_map.isLeftAvailable((int)x1, (int)x2, (int)x3, (int)x4, (int)(-y1), (int)(-y2), (int)(-y3),
                        (int)(-y4)))
                {
                    x1 -= 1;
                    x2 -= 1;
                    x3 -= 1;
                    x4 -= 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && _canMove)
        {
            if (x1 < 9.5 && x2 < 9.5 && x3 < 9.5 && x4 < 9.5)
                if (_map.isRightAvailable((int)x1, (int)x2, (int)x3, (int)x4, (int)(-y1), (int)(-y2), (int)(-y3),
                        (int)(-y4)))
                {
                    x1 += 1;
                    x2 += 1;
                    x3 += 1;
                    x4 += 1;
                }
        }

        if ((currentTime - startTime > delay)&& _canMove)
        {
            startTime += delay;
            y1 -= 1;
            y2 -= 1;
            y3 -= 1;
            y4 -= 1;
            if (!_map.MapUpdate((int)x1, (int)x2, (int)x3, (int)x4, (int)(-y1), (int)(-y2), (int)(-y3), (int)(-y4)))
            {
                DestroyFullRow();
                _canMove = false;
            }
        }
        if (_speedMove && _canMove)
        {
            y1 -= 1;
            y2 -= 1;
            y3 -= 1;
            y4 -= 1;
            if (!_map.MapUpdate((int)x1, (int)x2, (int)x3, (int)x4, (int)(-y1), (int)(-y2), (int)(-y3), (int)(-y4)))
            {
                DestroyFullRow();
                _canMove = false;
            }

            _speedMove = false;
        }
        DestroyFullRow();
        currentTime += Time.deltaTime;
    }

    private void DestroyFullRow()
    {
        
        if ((int)(-y1) == _map.rowToDestroy)
        {
            
            _map.changeValue((int)(-y1),(int)x1);
            Destroy(_box1);
            x1 = 100;
            y1 = -100;
        }

        if ((int)(-y2) == _map.rowToDestroy)
        {
            _map.changeValue((int)(-y2),(int)x2);
            Destroy(_box2);
            x2 = 100;
            y2 = -100;
        }

        if ((int)(-y3) == _map.rowToDestroy)
        {
            _map.changeValue((int)(-y3),(int)x3);
            Destroy(_box3);
            x3 = 100;
            y3 = -100;
        }

        if ((int)(-y4) == _map.rowToDestroy)
        {
            _map.changeValue((int)(-y4),(int)x4);
            Destroy(_box4);
            x4 = 100;
            y4 = -100;
        }
    }

    private  void ChangeMapVariable()
    {
    }
    

    private void RotateShape(int localShapeRotation)
    {
        switch (shapeIndex)
        {
            case 0:
                switch (localShapeRotation)
                {
                    case 0:
                        x1 += 1;
                        x3 -= 1;
                        x4 -= 2;
                        y1 += 1;
                        y3 -=1;
                        y4 -= 2;
                        break;
                    case 1:
                        x1 += 1;
                        x3 -= 1;
                        x4 -= 2;
                        y1 -= 1;
                        y3 += 1;
                        y4 += 2;
                        break;
                    case 2:
                        x1 -= 1;
                        x3 += 1;
                        x4 += 2;
                        y1 -= 1;
                        y3 += 1;
                        y4 += 2;
                        break;
                    case 3:
                        x1 -= 1;
                        x3 += 1;
                        x4 += 2;
                        y1 += 1;
                        y3 -= 1;
                        y4 -= 2;
                        break;
                }
                break;
            case 1:
                switch (localShapeRotation)
                {
                    case 0:
                        x1 += 1;
                        y1 -= 1;
                        x3 -= 1;
                        y3 -= 1;
                        x4 -= 2;
                        break;
                    case 1:
                        x1 -= 1;
                        y1 -= 1;
                        x3 -= 1;
                        y3 += 1;
                        y4 += 2;
                        break;
                    case 2:
                        x1 -= 1;
                        y1 += 1;
                        x3 += 1;
                        y3 += 1;
                        x4 += 2;
                        break;
                    case 3:
                        x1 += 1;
                        y1 += 1;
                        x3 += 1;
                        y3 -= 1;
                        y4 -= 2;
                        break;
                }
                break;
            case 2:
                switch (localShapeRotation)
                {
                    case 0:
                        x1 += 1;
                        y1 -= 1;
                        x3 -= 1;
                        y3 += 1;
                        x4 -= 2;
                        break;
                    case 1:
                        x1 -= 1;
                        y1 -= 1;
                        x3 += 1;
                        y3 += 1;
                        y4 += 2;
                        break;
                    case 2:
                        x1 -= 1;
                        y1 += 1;
                        x3 += 1;
                        y3 -= 1;
                        x4 += 2;
                        break;
                    case 3:
                        x1 += 1;
                        y1 += 1;
                        x3 -= 1;
                        y3 -= 1;
                        y4 -= 2;
                        break;
                }
                break;
            case 3:
                switch (localShapeRotation)
                {
                    case 0:
                        x1 += 1;
                        y1 += 1;
                        x3 -= 1;
                        y3 -= 1;
                        x4 -= 1;
                        y4 += 1;
                        break;
                    case 1:
                        x1 += 1;
                        y1 -= 1;
                        x3 -= 1;
                        y3 += 1;
                        x4 += 1;
                        y4 += 1;
                        break;
                    case 2:
                        x1 -= 1;
                        y1 -= 1;
                        x3 += 1;
                        y3 += 1;
                        x4 += 1;
                        y4 -= 1;
                        break;
                    case 3:
                        x1 -= 1;
                        y1 += 1;
                        x3 += 1;
                        y3 -= 1;
                        x4 -= 1;
                        y4 -= 1;
                        break;
                }
                break;
            case 4:
                switch (localShapeRotation)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
        }
        shapeRotation++;
        if (shapeRotation == 4)
            shapeRotation = 0;
    }

}
