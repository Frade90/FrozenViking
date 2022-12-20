using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public float health;
    public float previousHealth;
    public float maxHealth;
    public Image filler;
    public float counter;
    public float maxCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //ground testi, eli ollaanko kosketuksissa maahan vai ei.
        if(Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded= false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            //meill� on a tai d pohjassa
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else

        {
            //t�m� ajetaan kun ollaan pys�hdyksess�
            animator.SetBool("Walk", false);
        }

        //hyppy
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
        }

        if(counter > maxCounter)
        {
            previousHealth = health;
            counter = 0;    
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth / maxHealth, health / maxHealth, counter / maxCounter);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // ollaan osuttu ansaan -> v�hennet��n healthia.
            TakeDamage(20);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddHealth"))
        {
            //pelaaja on koskettanut syd�nt�.Tuhotaan syd�n ja lis�t��n 30 healthia.
            Destroy(collision.gameObject); //tuhotaan syd�n
            Heal(30);
        }
        if (collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(40);
        }
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }


    void AddMaxHealth(float AddMaxHealthAmount)
    {
        maxHealth += AddMaxHealthAmount;
    }

    void Heal(float healAmount)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        health += healAmount;

        //vaihtoehto 1
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void TakeDamage(float dmg)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        health -= dmg;
    }

}
