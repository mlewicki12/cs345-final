
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public PlayerController.PlayerClass Class;
    public GameObject PlayerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        var player = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        player.GetComponent<PlayerController>().Class = Class;
    }
}
