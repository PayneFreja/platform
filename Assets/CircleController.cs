using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class CircleController : MonoBehaviour
{

    [SerializeField]
    float speed = 5;

    [SerializeField]
    float jumpforce = 100;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundRadius = 0.1f;

    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    LayerMask groundLayer;
    bool mayJump = true;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movementX = new Vector2(moveX, 0);

        Vector2 movement = movementX;

        transform.Translate(movement * speed * Time.deltaTime);

        Vector3 size = MakeGroundcheckSize();
        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);


        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && isGrounded == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpforce;
            rb.AddForce(jump);
            anim.SetBool("isJumping", false);
            mayJump = false;

        }
        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        Vector3 size = MakeGroundcheckSize();
        Gizmos.DrawWireCube(groundCheck.position, size);
    }

    private Vector3 MakeGroundcheckSize() => new Vector3(1, groundRadius);

}
