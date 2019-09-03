
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClass {
        Mage,
        Knight
    }

    [Serializable]
    public class PlayerControls
    {
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }

    public float Speed;
    public float RotationSpeed;
    public PlayerClass Class;
    public GameObject FireSource;
    public Vector3 SpellSpeed;
    public PlayerControls MageControls;
    public PlayerControls KnightControls;

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

        GetMove();
            
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

    void GetMove()
    {
        float[] move;
        if (Class == PlayerClass.Mage)
        {
            move = GetDirections(MageControls);
            transform.Translate(new Vector3(move[0], 0, move[1]));
            
            FollowMouse();
        }
        else
        {
            move = GetDirections(KnightControls);
            transform.Translate(new Vector3(0, 0, move[1]));

            if (move[0] > 0)
            {
                transform.Rotate(new Vector3(0, 1, 0), RotationSpeed);
            } else if (move[0] < 0)
            {
                transform.Rotate(new Vector3(0, 1, 0), -RotationSpeed);
            }
        }
    }

    float[] GetDirections(PlayerControls controls)
    {
        KeyCode forward = controls.forward, back = controls.back,
            left = controls.left, right = controls.right;
        
        var ret = new float[2];
        
        ret[0] = ret[1] = 0.0f;

        if (Input.GetKey(right))
        {
            ret[0] += Speed;
        }

        if (Input.GetKey(left))
        {
            ret[0] -= Speed;
        }
        
        if (Input.GetKey(forward))
        {
            ret[1] += Speed;
        }

        if (Input.GetKey(back))
        {
            ret[1] -= Speed;
        }

        ret[0] *= Time.deltaTime;
        ret[1] *= Time.deltaTime;

        return ret;
    }
}
