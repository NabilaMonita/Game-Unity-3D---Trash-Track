using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void play(string SampleScene)
    {
        Application.LoadLevel(SampleScene);
    }

    public void sound_volume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
}
