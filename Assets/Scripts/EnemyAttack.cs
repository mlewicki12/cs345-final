
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float KnockbackStrength;
    public float YKnockback;

    private EntityInfo _info;

    void Start()
    {
        _info = GetComponent<EntityInfo>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var _rigidbody = other.gameObject.GetComponent<Rigidbody>();
            
            var vel = (transform.position + other.transform.position).normalized;
            vel *= KnockbackStrength;
            vel.y = YKnockback;

            _rigidbody.velocity = vel;

            _rigidbody = GetComponent<Rigidbody>();
            vel *= -0.25f;
            vel.y *= -1;
            
            _rigidbody.velocity = vel;

            other.gameObject.GetComponent<EntityInfo>().InflictDamage(_info.Damage);
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            var spell = other.gameObject.GetComponent<Projectile>();
            _info.InflictDamage(spell.Damage);
        }
    }
}
