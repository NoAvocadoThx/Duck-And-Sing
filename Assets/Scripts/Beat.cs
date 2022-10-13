using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] internal float BPM = 200;
     private float beat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBPM();

        Time.timeScale = beat;
    }

    void ChangeBPM()
    {
        if (BPM <= 200 && BPM > 150)
        {
            beat = 1;
        }
        else if (BPM <= 150 && BPM > 110)
        {
            beat = 0.5f;

        }
    }
}
