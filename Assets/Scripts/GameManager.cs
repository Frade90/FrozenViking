using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public float health;
    public float previousHealth;
    public float maxHealth;

    public float historyHealth;
    public float historyPrevHealth;
    public float historyMaxHealth;

    public string previousLevel;
    public string currentLevel;

    //jokaista tasoa varten on boolean muuttuja. t�rke��= muuttujan nimi pit�� olla sama kuin LoadLevel Scriptiss� olevan LevelToLoad muuttujan arvo.

    public bool Level1;
    public bool Level2;
    public bool Level3;

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Map")
            {
                SceneManager.LoadScene("MainMenu");

            }

        }
    }

    public void Save()
    {
        Debug.Log("Game Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        //sy�tet��n tallennettava tieto data-objektiin, joka lopuksi serialisoidaan
        data.health = health;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.currentLevel = currentLevel;
        bf.Serialize(file, data);
        file.Close();

    }

    public void Load()
    {
      //tarkastetaan, onko olemassa tallennustiedostoa. Jos on, niin sitten voidaan ladata tiedot.
      if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //siirret��n tiedot PlayerDatasta meid�n GameManageriin

            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
            currentLevel = data.currentLevel;
        }



    }


}
[Serializable]
class PlayerData
{
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;
    public bool Level1;
    public bool Level2;
    public bool Level3;
}
