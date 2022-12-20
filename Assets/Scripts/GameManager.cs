using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private void Awake()
    {
        //aivan ensin pelin k�ynnistyess� ajetaan t�m� funktio. t�ll� luodaan game manager
        //singleton
        //tarkistetaan, onko manager jo olemassa
        if(manager == null)
        {
            //jos ei ole manageria, kerrotaan ett� t�m� luokka on se pelin manageri
            //kerrotaan my�s, ett� t�m� manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //t�m� ajetaan silloin jos on jo olemassa manageri ja ollaan luomassa toinen manageri. se on liikaa
            //t�ll�in t�m� manageri tuhotaan pois, jolloin j�� vain ensinm�inen
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
