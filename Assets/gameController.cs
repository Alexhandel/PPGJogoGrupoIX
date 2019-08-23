using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface pausable
{
    bool isPaused { get; set; }
}
public class gameController : MonoBehaviour, pausable
{
    public GameObject player, enemy;
    public GameObject lossUI, victoryUI, screenFlashPanelUI, pauseUI;
    private GameObject[] gameObjects, gameObjects2;

    public bool isPaused { get ; set ; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown("c"))
            {
                destroyAllEnemyAttacks();
            }
            if (player.GetComponent<playercontroller>().alive == false)
            {
                activateLossScreen();
            }
            if (enemy.GetComponent<enemyController>().alive == false)
            {
                activateVictoryScreen();
            }
            if (Input.GetKeyDown("escape"))
            {
                gamePause();
            }
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
    void destroyAllEnemyAttacks()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("attack");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        screenFlashPanelUI.SetActive(true);    
    }
    public void gameReset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    void gamePause()
    {
        pauseUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        gameObjects2 = FindObjectsOfType<GameObject>();
        for (var i = 0; i < gameObjects2.Length; i++)
        {
            if (gameObjects2[i].GetComponent<pausable>()!=null)
            {
                gameObjects2[i].GetComponent<pausable>().isPaused = true;
            }
            
        }
    }
    public void gameUnpause()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        gameObjects2 = FindObjectsOfType<GameObject>();
        Time.timeScale = 1f;
        for (var i = 0; i < gameObjects2.Length; i++)
        {
            if (gameObjects2[i].GetComponent<pausable>() != null)
            {
                gameObjects2[i].GetComponent<pausable>().isPaused = false;
            }

        }
    }
}
