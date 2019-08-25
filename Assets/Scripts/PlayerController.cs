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
    private Vector3 _worldPos;
    private Vector3 _speed;

    public GameObject MageSpell; // there's gotta be a better way to store spells
    // actually, GameObject[], allowing for players to switch spells

    void Start()
    {
        _timeToFire = 0f;
        _speed = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _timeToFire = Math.Max(0f, _timeToFire - Time.deltaTime);
        
        FollowMouse();
        
        float moveX = Input.GetAxis("Horizontal") * Acceleration; // AD
        float moveZ = Input.GetAxis("Vertical") * Acceleration; // WS

        moveX *= Time.deltaTime;
        moveZ *= Time.deltaTime;

        _speed = new Vector3(moveX, 0, moveZ);
        transform.Translate(_speed);

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
         var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,
             Input.mousePosition.y,
             0));
         
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, 100))
         {
             var dir = hit.point;
             dir.y = transform.position.y;
             
             transform.LookAt(dir);
         }
     }

    void FireSpell()
    {
        var spell = Instantiate(MageSpell, FireSource.transform.position, transform.rotation);
        spell.GetComponent<Rigidbody>().velocity = SpellSpeed.x * transform.right + SpellSpeed.z * transform.forward;
        
        Destroy(spell, ProjectileLifeTime);
    }
}
