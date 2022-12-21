using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadMap()
    {
        //kun klikataan play-painiketta menussa -> siirrytään Map sceneen.
        SceneManager.LoadScene("Map");


    }

    public void Save()
    {
        //tämä ajetaan menusta, kun painetaan save

        GameManager.manager.Save();
    }

    public void Load()
    {
        GameManager.manager.Load();
    }

    public void QuitGame()
    {
        Application.Quit();

    }

}
