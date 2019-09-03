
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject MagePrefab;
    public GameObject KnightPrefab;

    public float MageOffset;
    public float KnightOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        var position = transform.position;
        var rotation = transform.rotation;

        var player = Instantiate(MagePrefab, position + new Vector3(0, MageOffset, 0), rotation);
        player.GetComponent<PlayerController>().Class = PlayerController.PlayerClass.Mage;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.name = "MagePlayer";

        player = Instantiate(KnightPrefab, position - new Vector3(KnightOffset, 0, 0), rotation);
        player.GetComponent<PlayerController>().Class = PlayerController.PlayerClass.Knight;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.name = "KnightPlayer";
    }
}
