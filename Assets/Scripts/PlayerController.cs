using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Speed; // AD
        float moveZ = Input.GetAxis("Vertical") * Speed; // WS

        moveX *= Time.deltaTime;
        moveZ *= Time.deltaTime;

        transform.Translate(-1 * moveX, 0.0f, -1 * moveZ);
    }
}
