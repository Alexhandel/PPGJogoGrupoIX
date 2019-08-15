using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject player, enemy;
    public GameObject lossUI, victoryUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<playercontroller>().alive==false)
        {
            activateLossScreen();
        }
        if (enemy.GetComponent<enemyController>().alive==false)
        {
            activateVictoryScreen();
        }
    }
    void activateLossScreen()
    {
        lossUI.SetActive(true);   
    }
    void activateVictoryScreen()
    {
        victoryUI.SetActive(true);
    }
}
