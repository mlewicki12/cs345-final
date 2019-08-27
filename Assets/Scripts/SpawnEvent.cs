
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public GameObject Enemy;
    public int Amount;
    
    public Vector3 StartPos;
    public float DistanceBetween;
    public float OffsetY;
    
    public GameObject NextEvent;

    void Start()
    {
        float xAdd = 0f;
        
        for (int i = 0; i < Amount; ++i)
        {
            Vector3 pos = StartPos;
            pos.x += xAdd;
            pos.y += OffsetY;
            
            Instantiate(Enemy, pos, Quaternion.identity);
            xAdd += DistanceBetween;
        }

        if (NextEvent != null)
        {
            Instantiate(NextEvent);
        }
        
        Destroy(gameObject);
    }
}
