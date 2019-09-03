
using UnityEngine;

public class WinEvent : MonoBehaviour
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
        
        Debug.Log("You win!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
