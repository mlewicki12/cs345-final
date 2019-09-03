
using System.Linq;
using UnityEngine;

public class TileEvent : MonoBehaviour
{
    public float Height;
    public float Speed;
    public GameObject[] Tiles;
    public Vector3[] Position;
    public GameObject NextEvent;

    private GameObject[] _tiles;
    private bool[] _done;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos;

        _tiles = new GameObject[Tiles.Length];
        _done = new bool[Tiles.Length];
        
        for (int i = 0; i < Tiles.Length; ++i)
        {
            startPos = Position[i];
            startPos.y += Height;
            
            _tiles[i] = Instantiate(Tiles[i], startPos, Tiles[i].transform.rotation);
            _done[i] = false;
        }
    }

    void Update()
    {
        if (_done.Any(x => !x))
        {
            for (int i = 0; i < _tiles.Length; ++i)
            {
                if (!_done[i])
                {
                    _tiles[i].transform.Translate(new Vector3(0, -Speed, 0));
                    if (_tiles[i].transform.position.y <= Position[i].y)
                    {
                        _tiles[i].transform.position = Position[i];
                        _done[i] = true;
                    }
                }
            }
        }
        else
        {
            if(NextEvent != null) {
                Instantiate(NextEvent);
            }

            Destroy(gameObject);
        }
    }
}
