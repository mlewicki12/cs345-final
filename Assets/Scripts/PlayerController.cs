
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClass {
        Mage,
        Knight
    }

    public float Speed;
    public PlayerClass Class;
    public GameObject FireSource;
    public Vector3 SpellSpeed;

    public float LookRange;
    public float FireTime;
    public float ProjectileLifeTime;
    
    private float _timeToFire;
    private Vector3 _worldPos;
    private ChatText _text;
    private Rigidbody _rigidbody;

    public GameObject MageSpell; // there's gotta be a better way to store spells
    // actually, GameObject[], allowing for players to switch spells

    void Start()
    {
        _timeToFire = 0f;
        _text = GetComponent<ChatText>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeToFire = Math.Max(0f, _timeToFire - Time.deltaTime);
        _rigidbody.velocity = new Vector3(0, 0, 0);

        float[] move;
        if (Class == PlayerClass.Mage)
        {
            move = GetMove(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
            
            transform.Translate(new Vector3(move[1], 0, move[0]));
            FollowMouse();
        }
        else
        {
            move = GetMove(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
            Vector3 toPoint = new Vector3(move[1], 0, move[0]);
            
            transform.Translate(toPoint);
            
            transform.LookAt(transform.position + GetNewDir(toPoint));
        }

            
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

             var comp = transform.position - dir;
             if (comp.magnitude >= LookRange)
             {
                 transform.LookAt(dir);
             }
         }
     }

    void FireSpell()
    {
        var spell = Instantiate(MageSpell, FireSource.transform.position, transform.rotation);
        spell.GetComponent<Rigidbody>().velocity = SpellSpeed.x * transform.right + SpellSpeed.z * transform.forward;
        
        Destroy(spell, ProjectileLifeTime);
    }

    float[] GetMove(KeyCode forward, KeyCode back, KeyCode left, KeyCode right)
    {
        var ret = new float[2];
        ret[0] = ret[1] = 0.0f;

        if (Input.GetKey(forward))
        {
            ret[0] += Speed;
        }

        if (Input.GetKey(back))
        {
            ret[0] -= Speed;
        }

        if (Input.GetKey(right))
        {
            ret[1] += Speed;
        }

        if (Input.GetKey(left))
        {
            ret[1] -= Speed;
        }

        ret[0] *= Time.deltaTime;
        ret[1] *= Time.deltaTime;
        
        return ret;
    }

    Vector3 GetNewDir(Vector3 point)
    {
        Vector3 ret = new Vector3(0, 0, 0);
        if (point.x < 0)
        {
            ret.x = 1;
        } else if (point.x < 0)
        {
            ret.x = -1;
        }

        if (point.z < 0)
        {
            ret.z = 1;
        } else if (point.z > 0)
        {
            ret.z = -1;
        }

        return ret;
    }
}
