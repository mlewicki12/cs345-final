
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject MagePrefab;
    public GameObject KnightPrefab;

    public float MageOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        var position = transform.position;
        var rotation = transform.rotation;

        var player = Instantiate(MagePrefab, position + new Vector3(0, MageOffset, 0), rotation);
        player.GetComponent<PlayerController>().Class = PlayerController.PlayerClass.Mage;
        player.name = "MagePlayer";

        player = Instantiate(KnightPrefab, position + new Vector3(1, 0, 0), rotation);
        player.GetComponent<PlayerController>().Class = PlayerController.PlayerClass.Knight;
        player.name = "KnightPlayer";
    }
}
