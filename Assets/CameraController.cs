using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    //får cameran att hela tiden följa efter spelarens position
    void Update()
    {
        transform.position = new Vector3(
        player.transform.position.x,
         player.transform.position.y,
        -10
        );

    }
}
