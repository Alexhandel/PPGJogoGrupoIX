using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string uiButtonSound = "event:/SFX/UI/DefaultButtonAction";
    [FMODUnity.EventRef]
    public string music = "event:/Music/Menu";
    FMOD.Studio.EventInstance musicEv;
    public GameObject difficultyKeeper, startUI, difficultyUI, controlsUI;

    // Start is called before the first frame update
    void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
        difficultyKeeper.GetComponent<difficultyKeeeper>().hasParent = true;
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
    public void showControlUI()
    {
        startUI.SetActive(false);
        controlsUI.SetActive(true);
    }
    public void hideControlUI()
    {
        controlsUI.SetActive(false);
        startUI.SetActive(true);
        
    }
    public void playUISound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiButtonSound, transform.position);
    }
    public void appExit()
    {
        Application.Quit();
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
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth = 9;
            difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth = 10;
            difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber = 2;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed = 1f;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer = 1.5f;

        }
        else if (diffi == "hard")
        {
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth = 12;
            difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth = 8;
            difficultyKeeper.GetComponent<difficultyKeeeper>().bombNumber = 1;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed = 0.8f;
            difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer = 1f;

        }
        musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
