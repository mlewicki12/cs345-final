
using UnityEngine;

public class PlayerDeathEvent : MonoBehaviour
{
    private GameObject _mage;
    private GameObject _knight;
    
    // Start is called before the first frame update
    void Start()
    {
        _mage = GameObject.Find("MagePlayer");
        _knight = GameObject.Find("KnightPlayer");
        
        Destroy(_mage);
        Destroy(_knight);
        
        Debug.Log("You lose!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
