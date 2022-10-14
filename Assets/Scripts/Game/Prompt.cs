using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float PROMPT_TIME_DURATION = 4; // how long the prompt appear on screen
    [SerializeField] private float PROMPT_TIME_INTERVAL = 1; // how long next prompt will show up
    [SerializeField] private float PROMPT_TIME_DURATION_MINIMAL = 2; // minimal time the prompt appear on screen 
    [SerializeField] private float PROMPT_TIME_INTERVAL_MINIMAL = 0.5f; // how long next prompt will show up on min time
    [SerializeField] private float ANGER_FACTOR = 0.4f;

    [Header("Prompt Location")]
    [SerializeField] private List<Transform> PromptLocationList;

    // public 
    [Header("Other")]
    public List<GameObject> PromptPool;
    public Kid KidObject;
    public Duck DuckObject;

    // private
    private float AngerValue = 0;
    private float PromptDurationTimer = 0;
    private float PromptIntervalTimer = 0;
    private int PromptCount = 0;
    private List<GameObject> PromptList;
    private float PromptTimeInterval;
    private float PromptTimeDuration;

    

  

    // Start is called before the first frame update
    void Start()
    {
        PromptDurationTimer = PROMPT_TIME_DURATION;
        PromptIntervalTimer = PROMPT_TIME_INTERVAL;
        PromptCount = 0;
        AngerValue = 0;
        PromptList = new List<GameObject> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(KidObject != null)
        {
            AngerValue = KidObject.GetAngerValue();
        }
        PromptTimeInterval = Mathf.Max(PROMPT_TIME_INTERVAL_MINIMAL, PROMPT_TIME_INTERVAL - ANGER_FACTOR * AngerValue / 100);
        PromptTimeDuration = Mathf.Max(PROMPT_TIME_DURATION_MINIMAL, PROMPT_TIME_DURATION - PROMPT_TIME_DURATION * AngerValue / 100);

        PromptDurationTimer -= Time.deltaTime;
        if (PromptDurationTimer <= 0 )
        {
            for(int i = 0; i< PromptList.Count;i++ )
            {
                Destroy ( PromptList[i]);
            }
            PromptList.Clear ();


            PromptIntervalTimer -= Time.deltaTime;
            if (PromptIntervalTimer <= 0)
            {

                PromptCount = Random.Range((int)2, (int)PromptLocationList.Count + 1);
                for (int i = 0; i < PromptCount; i++)
                {
                    GameObject Note = Instantiate(PromptPool[Random.Range(0, PromptPool.Count)], PromptLocationList[i].transform);
              
                    PromptList.Add(Note);
                }
                PromptDurationTimer = PromptTimeDuration;
                PromptIntervalTimer = PromptTimeInterval;
            }
        }
    }
}
