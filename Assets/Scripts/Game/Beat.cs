using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Beat : MonoBehaviour
{
    [SerializeField] private Kid KidObject;
    [SerializeField] internal float beat;
   
    private AudioSource BGMusic;
    private bool IsBGMPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        BGMusic = GetComponent<AudioSource>();
        IsBGMPlaying = false;



    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameStarted() && !IsBGMPlaying)
        {
            BGMusic.Play();
            IsBGMPlaying = true;
        }

        if(GameManager.Instance.IsGameEnded() && IsBGMPlaying )
        {
            BGMusic.Stop();
            IsBGMPlaying = false;
        }

        ChangeBPM();

 
    }

    void ChangeBPM()
    {
        if (KidObject.KidAngryLevel == ANGER_LEVEL.NONE)
        {
            beat = 1.0f;
        }
        else if (KidObject.KidAngryLevel == ANGER_LEVEL.LOW)
        {
            beat = 1.1f;

        }
        else if (KidObject.KidAngryLevel == ANGER_LEVEL.MEDIUM)
        {
            beat = 1.2f;

        }
        else if (KidObject.KidAngryLevel == ANGER_LEVEL.HIGH)
        {
            beat = 1.3f;

        }
        else if (KidObject.KidAngryLevel == ANGER_LEVEL.FEVER)
        {
            beat = 1.5f;

        }

        BGMusic.pitch = beat;
       
    }
}
