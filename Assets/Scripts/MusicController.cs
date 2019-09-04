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
        musicEv.start();
    }

    public void setGamePaused()
    {
        
    }

    // Player has clicked "Play Now"
    /*public void GameStartedMusic()
    {
        musicEv.setParameterByName("GameStarted", 1f);
    }

    // Player is less than 50% health
    public void LoHealthMusic()
    {
        musicEv.setParameterByName("LoHealth", 1f);
    }

    public void IsDeadMusic()
    {
        musicEv.setParameterByName("IsDead", 1f);
    }

    public void SetHeartbeat(float value)
    {
        playerHeartbeatEv.setParameterByName("Health", value);
    }
    */
    // Update is called once per frame
    void Update()
    {

    }
}
