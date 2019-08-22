using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Destructable : MonoBehaviour
{
    public GameObject Fill;
    public GameObject Parent;
    public Material Colour;

    // hardcoding it for now
    public float XOffset;
    public float YOffset;
    public float ZOffset;

    public float MaxSpeed;
    public float FillSize;

    public int Health;
    
    public float MinDeathTime;
    public float MaxDeathTime;

    private bool _replaced;

    private Vector3 _size;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _replaced = false;
        _size = GetComponent<Collider>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(XOffset, YOffset, ZOffset);

        if (!_replaced && Health <= 0)
        {
            Destroy(gameObject);
            
            // beautiful triple for loop here
            for (float i = 0; i < _size.x; i += FillSize)
            {
                for (float j = 0; j < _size.y; j += FillSize)
                {
                    for (float k = 0; k < _size.z; k += FillSize)
                    {
                        // doesn't spawn in the same position
                        var obj = Instantiate(Fill, transform.position + offset + new Vector3(i, j, k), Quaternion.identity);
                        obj.GetComponent<Renderer>().material = Colour;
                        obj.transform.parent = Parent.transform;

                        Vector3 vel = new Vector3(Random.Range(-1 * MaxSpeed, MaxSpeed), 
                                                  Random.Range(0, MaxSpeed),
                                                  Random.Range(-1 * MaxSpeed, MaxSpeed));

                        obj.GetComponent<Rigidbody>().velocity = vel;
                        Destroy(obj, Random.Range(MinDeathTime, MaxDeathTime));
                    }
                }
            }

            _replaced = true;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile oth = other.gameObject.GetComponent<Projectile>();
            Health -= oth.Damage;
            
            Destroy(other.gameObject);
        }
    }
}
