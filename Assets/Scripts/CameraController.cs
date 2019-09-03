
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MoveDistance = 8.0f;
    
    private GameObject _mage;
    private GameObject _knight;

    private bool _set;
    private Vector3 _center;
    private Vector3 _mainPos;

    public void GetPlayers()
    {
        _mage = GameObject.Find("MagePlayer");
        _knight = GameObject.Find("KnightPlayer");
        _set = true;
    }

    void Start()
    {
        _set = false;
        
        _mainPos = transform.position;
        _mainPos.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_set)
        {
            _center = Vector3.Lerp(_mage.transform.position, _knight.transform.position, 0.5f);
            _center.y = 0;

            Vector3 dis = _center - _mainPos;
            dis.y = 0;
            if (dis.magnitude >= MoveDistance)
            {
                transform.position += dis.normalized * Time.deltaTime;
                
                _mainPos = transform.position;
                _mainPos.y = 0;
            }
            else
            {
                transform.LookAt(_center);
            }

        }
    }
}
