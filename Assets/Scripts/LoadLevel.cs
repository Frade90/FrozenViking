using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string levelToLoad;

    public bool cleared; //t‰m‰ on true jos kentt‰ on p‰‰sty l‰pi. muuten false.

    // Start is called before the first frame update
    void Start()
    {
        //katsotaan aina Map Scene avattaessa, ett‰ onko GameManagerissa merkattu, ett‰ kyseinen taso on l‰p‰isty
        //jos on l‰p‰isty, ajetaan cleared funktio, joka tekee muutokset t‰h‰n objektiin,
        //eli n‰ytt‰‰ Stage Clear kyltin.
        if(GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true); //koska rasti on olemassa,, merkataan taso l‰pik‰ydyksi.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if(isClear == true)
        {
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);

            //on m‰‰ritelty, ett‰ kentt‰ on p‰‰sty l‰pi. laitetaan stage clear kyltti n‰kyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //koska taso on p‰‰sty l‰pi. deaktivoidaan leveltriggerin circlecollider. n‰in tasoon ei p‰‰se
            //palaamaan myˆhemmin
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }

}
