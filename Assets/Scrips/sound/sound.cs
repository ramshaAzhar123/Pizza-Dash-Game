using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateSoundButtons();
        AudioListener.pause = false;
        soundOnBtn.SetActive(false);
        soundOffBtn.SetActive(true);
    }

    public void SoundOn()
    {
        AudioListener.pause = false;
        //UpdateSoundButtons();
        soundOffBtn.SetActive(true);
        soundOnBtn.SetActive(false);
    }

    public void SoundOff()
    {
        AudioListener.pause = true;
        //UpdateSoundButtons();
        soundOnBtn.SetActive(true);
        soundOffBtn.SetActive(false);
    }

    void UpdateSoundButtons()
    {
        bool isSoundOn = !AudioListener.pause;
        soundOnBtn.SetActive(isSoundOn);
        soundOffBtn.SetActive(!isSoundOn);
    }
}