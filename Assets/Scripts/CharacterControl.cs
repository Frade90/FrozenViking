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


    public Image filler;
    public float counter;
    public float maxCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.historyHealth = GameManager.manager.health;
        GameManager.manager.historyPrevHealth = GameManager.manager.previousHealth;
        GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;
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
            //meillä on a tai d pohjassa
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else

        {
            //tämä ajetaan kun ollaan pysähdyksessä
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
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;    
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);


        if(gameObject.transform.position.y < -20)
        {
            Die();
           
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // ollaan osuttu ansaan -> vähennetään healthia.
            TakeDamage(20);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddHealth"))
        {
            //pelaaja on koskettanut sydäntä.Tuhotaan sydän ja lisätään 30 healthia.
            Destroy(collision.gameObject); //tuhotaan sydän
            Heal(30);
        }
        if (collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(40);
        }
        if (collision.CompareTag("LevelEnd"))
        {
            GameManager.manager.previousLevel = GameManager.manager.currentLevel;
            SceneManager.LoadScene("Map");
        }
    }


    void AddMaxHealth(float AddMaxHealthAmount)
    {
        GameManager.manager.maxHealth += AddMaxHealthAmount;
    }

    void Heal(float healAmount)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health += healAmount;

        //vaihtoehto 1
        if(GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        }
    }

    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;

        if(GameManager.manager.health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPrevHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("Map");
    }

}
