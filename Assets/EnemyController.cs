using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundRadius = 0.1f;

    [SerializeField]
    int health = 3;

    float timeBetweenAttack = 1;
    float timer = 0;
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    LayerMask groundLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // metod som ser till att enemyn inte faller igenom marken
    void Update()
    {

        timer += Time.deltaTime;

        Vector3 size = MakeGroundcheckSize();
        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //om enemyn kolliderar med ett objekt med taggen "player" så ska attack animationen spelas upp
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && timer > timeBetweenAttack)
        {
            anim.SetTrigger("attack");
            timer = 0;
        }
    }

    //if sats som kollar om enemyn kolliderar med ett objekt med taggen "playersword" så ska den tappa hälsa
    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerSword")
        {
            health--;
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
