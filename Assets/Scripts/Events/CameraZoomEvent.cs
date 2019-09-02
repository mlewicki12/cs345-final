
using UnityEngine;

public class CameraZoomEvent : MonoBehaviour
{
    public float DesiredZoom;
    public float Speed;
    public GameObject NextEvent;
    
    private Camera _camera;
    private bool _dir;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (_camera.orthographicSize < DesiredZoom)
        {
            _dir = true;
        }
        else _dir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera.orthographicSize != DesiredZoom)
        {
            float newSize = _camera.orthographicSize + Speed;
            if ((_dir && newSize > DesiredZoom) || (!_dir && newSize < DesiredZoom))
            {
                newSize = DesiredZoom;
            }

            _camera.orthographicSize = newSize;
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
