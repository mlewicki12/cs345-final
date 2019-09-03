
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MagePlayer" || other.gameObject.name == "KnightPlayer")
        {
            Instantiate(Event);
            Destroy(gameObject);
        }
    }
}
