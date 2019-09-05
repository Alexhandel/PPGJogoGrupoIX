using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class storyController : MonoBehaviour
{
    public GameObject text;
    public float time, waitTime, fadeoutTime;
    public bool isDone=false;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            changeScene();
        }
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
        StartCoroutine(FadeImage());
    }
    IEnumerator FadeImage()
    {
        // loop over 1 second backwards
        for (float i = fadeoutTime; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            text.GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, i/fadeoutTime);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
