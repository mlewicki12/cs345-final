
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    public string Name;
    public int MaxHealth;
    public int Damage;

    private int _health;
    private bool _dead;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
        _dead = false;
    }

    void Update()
    {
        if (!_dead && MaxHealth != -1 && _health <= 0)
        {
            _dead = true;
            gameObject.GetComponent<ChatText>().Clear();
            
            var hp = gameObject.GetComponent<HealthBar>();
            if (hp != null)
            {
                hp.Hide();
            }
            
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public int InflictDamage(int dmg)
    {
        if (MaxHealth == -1)
        {
            return -1;
        }
        
        _health -= dmg;
        return _health;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            var spell = other.gameObject.GetComponent<Projectile>();
            InflictDamage(spell.Damage);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            var info = other.gameObject.GetComponent<EntityInfo>();
            info.InflictDamage(Damage);
        }
    }
}
