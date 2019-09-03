
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_set)
        {
            _center = Vector3.Lerp(_mage.transform.position, _knight.transform.position, 0.5f);
            transform.LookAt(_center);
        }
    }
}
