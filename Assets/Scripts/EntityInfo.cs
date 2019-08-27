
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    public string Name;
    public int MaxHealth;

    private int _health;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
    }

    public int GetHealth()
    {
        return _health;
    }
}
