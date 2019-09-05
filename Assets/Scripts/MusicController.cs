using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string music = "event:/Music/Gameplay";
    FMOD.Studio.EventInstance musicEv;

    // Start is called before the first frame update
    void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();

        //playerHeartbeatEv.setParameterByName("Health", 100f);
    }
    public void stopMusic()
    {
        musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void startMusic()
    {
        musicEv.setParameterByName("IsDead", 0f);
        musicEv.setParameterByName("IsWinner", 0f);
        musicEv.start();
    }

    // Player has clicked "Play Now"

    public void PlayLoserMusic()
    {
        musicEv.setParameterByName("isLoser", 1f);
    }

    public void PlayWinnerMusic()
    {
        musicEv.setParameterByName("IsWinner", 1f);
    }

    public void SetPaused(float value)
    {
        musicEv.setParameterByName("isPaused", value);
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
