using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public interface pausable
{
    bool isPaused { get; set; }
}
public class gameController : MonoBehaviour, pausable
{
    public GameObject player, enemy, camera1;
    public GameObject lossUI, victoryUI, screenFlashPanelUI, pauseUI, bombUI, numberUI;
    private GameObject[] gameObjects, gameObjects2;
    public int bombLimit;
    public float shakeDuration, shakeMagnitude;
    public Sprite number0, number1, number2, number3, number4, number5;
    public GameObject difficultyKeeper;

    public bool isPaused { get ; set ; }

    void Start()
    {
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
        bombLimit = difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber;
        //bombUI.GetComponent<TextMeshProUGUI>().text = "" + bombLimit;
        switch (bombLimit)
        {
            case 1:
                numberUI.GetComponent<Image>().sprite = number1;
                break;
            case 2:
                numberUI.GetComponent<Image>().sprite = number2;
                break;
            case 3:
                numberUI.GetComponent<Image>().sprite = number3;
                break;
            case 4:
                numberUI.GetComponent<Image>().sprite = number4;
                break;
            case 5:
                numberUI.GetComponent<Image>().sprite = number5;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if ((Input.GetKeyDown("l") || Input.GetKeyDown("x")) && bombLimit>0)
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
        camera1.GetComponent<screenShakeScript>().TriggerShake(shakeDuration, shakeMagnitude);
        //bombUI.GetComponent<TextMeshProUGUI>().text = "" + bombLimit;
        switch (bombLimit)
        {
            case 0:
                numberUI.GetComponent<Image>().sprite = number0;
                break;
            case 1:
                numberUI.GetComponent<Image>().sprite = number1;
                break;
            case 2:
                numberUI.GetComponent<Image>().sprite = number2;
                break;
            case 3:
                numberUI.GetComponent<Image>().sprite = number3;
                break;
            case 4:
                numberUI.GetComponent<Image>().sprite = number4;
                break;
            case 5:
                numberUI.GetComponent<Image>().sprite = number5;
                break;
            default:
                break;
        }
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
    public void appExit()
    {
        Application.Quit();
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
