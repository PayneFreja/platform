using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{

    [SerializeField]
    float speed = 4.1f;
    void Start()
    {

    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        // float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movementX = new Vector2(moveX, 0);
        // Vector2 movementY = new Vector2(0, moveY);

        Vector2 movement = movementX;

        transform.Translate(movement * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > 8.5f)
        {
            transform.Translate(-movementX * speed * Time.deltaTime);
        }

        // if (Mathf.Abs(transform.position.y) > 4.5f)
        // {
        //     transform.Translate(-movementY * speed * Time.deltaTime);
        // }

    }
}
