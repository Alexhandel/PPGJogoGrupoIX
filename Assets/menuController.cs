using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject difficultyKeeper, startUI, difficultyUI;
    // Start is called before the first frame update
    void Start()
    {
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void changeUI()
    {
        startUI.SetActive(false);
        difficultyUI.SetActive(true);
    }
    public void changeDifficulty(string diffi)
    {
        if (diffi=="easy")
        {
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth = 6;
            difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth = 15;
            difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber = 3;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed = 1.2f;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer = 2f;
        }
        else if (diffi=="medium")
        {
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth = 10;
            difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth = 10;
            difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber = 2;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed = 1f;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer = 1.5f;

        }
        else if (diffi == "hard")
        {
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth = 14;
            difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth = 8;
            difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber = 1;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed = 0.8f;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer = 1f;

        }
    }
}
