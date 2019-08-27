
using UnityEngine;

public class TileEvent : MonoBehaviour
{
    public float Height;
    public float Speed;
    public GameObject Tile;
    public Vector3 Position;
    
    public GameObject NextEvent;

    private GameObject _tile;
    private bool _ran;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = Position;
        startPos.y += Height;

        _ran = false;
        _tile = Instantiate(Tile, startPos, Quaternion.identity);
    }

    void Update()
    {
        if (_tile.transform.position.y > Position.y)
        {
            _tile.transform.Translate(new Vector3(0, -Speed, 0));
        }
        else
        {
            if (NextEvent != null)
            {
                Instantiate(NextEvent);
            }

            Destroy(gameObject);
        }
    }
}
