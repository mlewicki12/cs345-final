
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public GameObject Enemy;
    public int Amount;
    public string Name;
    
    public Vector3 StartPos;
    public float DistanceBetween;
    public float OffsetY;
    
    public GameObject NextEvent;
    public GameObject OnDeathEvent;

    private GameObject[] _enemies;

    void Start()
    {
        float xAdd = 0f;
        _enemies = new GameObject[Amount];
        
        for (int i = 0; i < Amount; ++i)
        {
            Vector3 pos = StartPos;
            pos.x += xAdd;
            pos.y += OffsetY;
            
            _enemies[i] = Instantiate(Enemy, pos, Quaternion.identity);
            _enemies[i].name = $"{Name}{i}";
            
            xAdd += DistanceBetween;
        }

        if (NextEvent != null)
        {
            Instantiate(NextEvent);
        }
    }

    void Update()
    {
        bool dead = true;
        for (int i = 0; i < Amount; ++i)
        {
            if (_enemies[i] != null)
            {
                dead = false;
                break;
            }
        }

        if (dead)
        {
            if (OnDeathEvent != null)
            {
                Instantiate(OnDeathEvent);
            }
            
            Destroy(gameObject);
        }
    }
}
