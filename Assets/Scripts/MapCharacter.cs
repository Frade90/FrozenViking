using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);

    }

    //tehd‰‰n koodi, kun pelaaja osuu leveltringgeriin avautuu tasohyppely peli
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger"))
        {
            //t‰m‰ ajetaan jos kartassa pelaaja osuu objektiin, jonka tag on "LevelTrigger"
            //haetaan Leveltrigger objektista LoadLevel scripti, ja katsotaan mik‰ on levelToLoad muuttujan arvo
            //ja k‰ynnistet‰‰n sen niminen Scene
            SceneManager.LoadScene(collision.GetComponent<LoadLevel>().levelToLoad);
        }
    }

}
