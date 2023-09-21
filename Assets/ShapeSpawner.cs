using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _shape1;
    [SerializeField] private GameObject _shape2;
    [SerializeField] private GameObject _shape3;
    [SerializeField] private GameObject _shape4;
    [SerializeField] private GameObject _shape5;
    public bool canSpawn = true;
    
    //Spawn object at start
    private void Start()
    {
        SpawnObject();
    }
    
    

    
    //Spawn new random shape
    public void SpawnObject()
    {
        System.Random random = new System.Random();

        if (canSpawn)
        {
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
}
