using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClass {
        Mage
    }

    public float Acceleration;
    public PlayerClass Class;
    public GameObject FireSource;
    public Vector3 Decceleration;
    public Vector3 MaxSpeed;
    public Vector3 SpellSpeed;

    public float FireTime;
    public float ProjectileLifeTime;
    
    private float _timeToFire;
    private float _cameraDiff;
    private Vector3 _worldPos;
    private Vector3 _speed;
    private Rigidbody _rigidbody;

    public GameObject MageSpell; // there's gotta be a better way to store spells
    // actually, GameObject[], allowing for players to switch spells

    void Start()
    {
        _timeToFire = 0f;
        _speed = new Vector3(0, 0, 0);
        _rigidbody = GetComponent<Rigidbody>();
    }

    Vector3 Min(Vector3 first, Vector3 second)
    {
        float x, z;

        if (Math.Abs(second.x) > first.x)
        {
            x = first.x * Math.Sign(second.x);
        }
        else x = second.x;

        if (Math.Abs(second.z) > first.z)
        {
            z = first.z * Math.Sign(second.z);
        }
        else z = second.z;

        return new Vector3(x, 0, z);
    }

    Vector3 Deccel(Vector3 inp, Vector3 speed)
    {
        speed = Decceleration * Time.deltaTime;
        
        if (inp.x < 0)
        {
            inp.x = Math.Min(inp.x + speed.x, 0);
        }
        else inp.x = Math.Max(inp.x - speed.x, 0);

        if (inp.z < 0)
        {
            inp.z = Math.Min(inp.z + speed.z, 0);
        }
        else inp.z = Math.Max(inp.z - speed.z, 0);

        return inp;
    }
    
    // Update is called once per frame
    void Update()
    {
        _timeToFire = Math.Max(0f, _timeToFire - Time.deltaTime);
        _cameraDiff = Camera.main.transform.position.z - transform.position.z;
        
        FollowMouse();
        
        float moveX = Input.GetAxis("Horizontal") * Acceleration; // AD
        float moveZ = Input.GetAxis("Vertical") * Acceleration; // WS

        moveX *= Time.deltaTime;
        moveZ *= Time.deltaTime;

        _speed += new Vector3(-moveX, 0, moveZ);
        _speed = Deccel(_speed, Decceleration);
        
        _rigidbody.velocity = Min(MaxSpeed, _speed);

        if (Input.GetMouseButton(0) && _timeToFire <= 0f)
        {
            Fire();
            _timeToFire = FireTime;
        }
    }
    
    void Fire() {
        switch (Class)
        {
            case PlayerClass.Mage:
                FireSpell();
                break;
        }
    }

     void FollowMouse()
     {
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                              Input.mousePosition.y,
                                                              _cameraDiff));

        transform.LookAt(new Vector3(_worldPos.x, transform.position.y, _worldPos.z));
     }

    void FireSpell()
    {
        var spell = Instantiate(MageSpell, FireSource.transform.position, transform.rotation);
        spell.GetComponent<Rigidbody>().velocity = SpellSpeed;
        
        Destroy(spell, ProjectileLifeTime);
    }
}
