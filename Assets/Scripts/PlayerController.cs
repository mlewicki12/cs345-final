using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClass {
        MAGE
    }

    public float Speed;
    public PlayerClass Class;
    public GameObject FireSource;
    public Vector3 SpellSpeed;

    public float FireTime;
    public float ProjectileLifeTime;
    
    private float timeToFire;
    private float cameraDiff;
    private Vector3 worldPos;

    public GameObject MageSpell; // there's gotta be a better way to store spells
    // actually, GameObject[], allowing for players to switch spells

    void Start()
    {
        timeToFire = 0f;
        cameraDiff = Camera.main.transform.position.y - transform.position.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        timeToFire = Math.Max(0f, timeToFire - Time.deltaTime);
        
        float moveX = Input.GetAxis("Horizontal") * Speed; // AD
        float moveZ = Input.GetAxis("Vertical") * Speed; // WS

        moveX *= Time.deltaTime;
        moveZ *= Time.deltaTime;

        transform.Translate(moveX, 0.0f, moveZ);

        if (Input.GetMouseButton(0) && timeToFire <= 0f)
        {
            Fire();
            timeToFire = FireTime;
        }
    }
    
    void Fire() {
        switch (Class)
        {
            case PlayerClass.MAGE:
                FireSpell();
                break;
        }
    }

     void FollowMouse()
     {
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                              cameraDiff,
                                                              Input.mousePosition.z));

        transform.LookAt(new Vector3(worldPos.x, transform.position.y, worldPos.z));
        // Seems to be only rotating 90 degrees
     }

    void FireSpell()
    {
        var spell = Instantiate(MageSpell, FireSource.transform.position, transform.rotation);
        spell.GetComponent<Rigidbody>().velocity = SpellSpeed;
        
        Destroy(spell, ProjectileLifeTime);
    }
}
