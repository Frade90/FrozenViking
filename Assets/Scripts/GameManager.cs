using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private void Awake()
    {
        //aivan ensin pelin käynnistyessä ajetaan tämä funktio. tällä luodaan game manager
        //singleton
        //tarkistetaan, onko manager jo olemassa
        if(manager == null)
        {
            //jos ei ole manageria, kerrotaan että tämä luokka on se pelin manageri
            //kerrotaan myös, että tämä manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //tämä ajetaan silloin jos on jo olemassa manageri ja ollaan luomassa toinen manageri. se on liikaa
            //tällöin tämä manageri tuhotaan pois, jolloin jää vain ensinmäinen
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
