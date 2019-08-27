
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public PlayerController.PlayerClass Class;
    public GameObject PlayerPrefab;
    public Canvas DrawCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        var player = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        player.GetComponent<PlayerController>().Class = Class;
        player.GetComponent<HealthBar>().DrawCanvas = DrawCanvas;
        player.GetComponent<ChatText>().DrawCanvas = DrawCanvas;
    }
}
