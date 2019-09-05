using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storyController : MonoBehaviour
{
    public GameObject text;
    public float time, waitTime;
    public bool isDone=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text.GetComponent<textTyper>().finished&&!isDone)
        {
            time = Time.time;
            isDone = true;
        }
        if (isDone&&(Time.time-time>=waitTime))
        {
            changeScene();
        }
    }
    public void changeScene()
    {
        SceneManager.LoadScene(1);
    }
}
