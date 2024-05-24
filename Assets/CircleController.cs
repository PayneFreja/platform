using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class CircleController : MonoBehaviour
{

    //sätter ens snabbhet till 5
    [SerializeField]
    float speed = 5;

    //sätter kraften man ska hoppa med till 100
    [SerializeField]
    float jumpforce = 100;

    public int maxHealth = 5;
    public int currentHealth;

    public HealthBar healthBar;
    public GameManagerController gameManager;
    private bool isDead = false;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundRadius = 0.1f;

    float timeBetweenAttack = 1;
    float timer = 0;

    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    LayerMask groundLayer;
    //bool som sätter mayjump till true
    bool mayJump = true;

    //bool som sätter facingright till sant 
    bool facingRight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movementX = new Vector2(moveX, 0);
        Vector2 movementx = movementX;
        transform.Translate(movementx * speed * Time.deltaTime);
        timer += Time.deltaTime;


        //if sats som kollar om spelarens movementX är mindre än 0 (går åt vänster)
        //och spelaren kollar åt höger då ska metoden flip köras 
        if (movementx.x < 0 && facingRight)
        {
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y);
            Flip();
        }

        //else if som kollar ifall if satsen ovan är falsk då ska flip metoden köras igen
        else if (movementx.x > 0 && facingRight == false)
        {
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y);
            Flip();
        }

        Vector3 size = MakeGroundcheckSize();
        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);


        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && isGrounded == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpforce;
            rb.AddForce(jump);
            mayJump = false;

        }
        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }

        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetFloat("moveX", moveX);
        anim.SetBool("isGrounded", isGrounded);


        //flip metod som flippar gubben från att kolla åt ena hållet till det andra
        void Flip()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingRight = facingRight == false;

        }

        //if sats som kollar ifall man vänsterklickar på musen så ska attack animationen köras
        if (Input.GetMouseButtonDown(0) && timer > timeBetweenAttack)
        {
            anim.SetTrigger("attack");
            timer = 0;
        }

        //kallar på gameOver funktionen från gameManager ifall health är lika med eller mindre än 0 
        //och ifall boolen isDead är false (alltså att spelaren inte redan är död)
        //"stänger av" spelar objektet när den dör
        //kallar på gameover metoden från gameManager
        if (currentHealth <= 0 && isDead == false)
        {
            //sätter boolen isDead till true
            isDead = true;
            gameObject.SetActive(false);
            gameManager.gameOver();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //if sats som kollar om spelaren kolliderar med ett objekt med taggen "enemysword" så ska man tappa hälsa
        if (other.gameObject.tag == "EnemySword")
        {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        //if sats som kollar om spelaren kolliderar med ett objekt med taggen "enemysword" så ska man tappa hälsa
        if (other.gameObject.tag == "food")
        {
            currentHealth++;
            healthBar.SetHealth(currentHealth);
            Destroy(other.gameObject);
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