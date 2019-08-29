using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public interface pausable
{
    bool isPaused { get; set; }
}
public class gameController : MonoBehaviour, pausable
{
    public GameObject player, enemy;
    public GameObject lossUI, victoryUI, screenFlashPanelUI, pauseUI, bombUI;
    private GameObject[] gameObjects, gameObjects2;
    public int bombLimit;
    public GameObject difficultyKeeper;

    public bool isPaused { get ; set ; }

    void Start()
    {
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
        bombLimit = difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber;
        bombUI.GetComponent<TextMeshProUGUI>().text = "x " + bombLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown("c") && bombLimit>0)
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
        bombLimit -= 1;
        gameObjects = GameObject.FindGameObjectsWithTag("attack");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        screenFlashPanelUI.SetActive(true);
        bombUI.GetComponent<TextMeshProUGUI>().text = "x " + bombLimit;
    }
    public void gameReset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void goToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
