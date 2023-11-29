using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;


    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            -10
        );

    }
}
