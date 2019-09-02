
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClass {
        Mage
    }

    public float Acceleration;
    public PlayerClass Class;
    public GameObject FireSource;
    public Vector3 SpellSpeed;

    public float FireTime;
    public float ProjectileLifeTime;
    
    private float _timeToFire;
    private Vector3 _worldPos;
    private Vector3 _speed;
    private ChatText _text;
    private Rigidbody _rigidbody;

    public GameObject MageSpell; // there's gotta be a better way to store spells
    // actually, GameObject[], allowing for players to switch spells

    void Start()
    {
        _timeToFire = 0f;
        _speed = new Vector3(0, 0, 0);
        _text = GetComponent<ChatText>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeToFire = Math.Max(0f, _timeToFire - Time.deltaTime);
        _rigidbody.velocity = new Vector3(0, 0, 0);
        
        FollowMouse();
        
        float moveX = Input.GetAxis("Horizontal") * Acceleration; // AD
        float moveZ = Input.GetAxis("Vertical") * Acceleration; // WS

        moveX *= Time.deltaTime;
        moveZ *= Time.deltaTime;

        _speed = new Vector3(moveX, 0, moveZ);
        transform.Translate(_speed);

        if (Input.GetMouseButtonDown(0))
        {
            if (_text.InConversation())
            {
                _text.Conversation.Skip();
            }
            else if(_timeToFire <= 0f)
            {
                Fire();
                _timeToFire = FireTime;
            }
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
