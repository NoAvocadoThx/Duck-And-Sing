using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float PROMPT_INIT_SHOW_TIME = 2; // how long the prompt first appear on screen
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
    private float PromptTimeInterval;
    private float PromptTimeDuration;
    private int PromptCount = 0;
    private List<GameObject> PromptList;
    private bool _IsPromptShowing = false;

    private int CurrentProcessingActionIndex = 0; // on which prompt player is at
    private bool HasPlayerCompletedAllPrompt = false;
    //private int HighestStreak = 0;
    private int PlayerInputNoteIndex = 0;
    private bool HasSubscribedToEvent = false;
    private bool HasPlayerSing = false;
    private Animator NoteAnimator;




    // Start is called before the first frame update
    void Start()
    {
        PromptDurationTimer = PROMPT_INIT_SHOW_TIME;
        PromptIntervalTimer = PROMPT_TIME_INTERVAL;
        PromptCount = 0;
        AngerValue = 0;
        CurrentProcessingActionIndex = 0; 
        HasPlayerCompletedAllPrompt = false;
       // HighestStreak = 0;
        PlayerInputNoteIndex = 0;
        _IsPromptShowing = false;
        HasSubscribedToEvent = false;
        HasPlayerSing = false;
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
        // check if duration is up
        if (PromptDurationTimer <= 0 )
        {
            _IsPromptShowing = false;
            // clear all current prompts and reset stuff
            for (int i = 0; i< PromptList.Count; i++ )
            {
                if (PromptList[i] != null)
                {
                    Destroy(PromptList[i]);
                }
            }
            PromptList.Clear ();
            CurrentProcessingActionIndex = 0;

            PromptIntervalTimer -= Time.deltaTime;
            // check if interval between each prompt is up
            if (PromptIntervalTimer <= 0)
            {

                PromptCount = Random.Range((int)2, (int)PromptLocationList.Count + 1);
                for (int i = 0; i < PromptCount; i++)
                {
                    GameObject Note = Instantiate(PromptPool[Random.Range(0, PromptPool.Count)], PromptLocationList[i].transform);
                    PromptList.Add(Note);
                }
                _IsPromptShowing = true;
                PromptDurationTimer = PromptTimeDuration;
                PromptIntervalTimer = PromptTimeInterval;
            }
        }

        // we subscribe to the event only when our prompt is showing
        if(_IsPromptShowing && !HasSubscribedToEvent )
        {
            EventManager.OnSingAction1 += PressedSingAction1;
            EventManager.OnSingAction2 += PressedSingAction2;
            EventManager.OnSingAction3 += PressedSingAction3;
            EventManager.OnSingAction4 += PressedSingAction4;
            HasSubscribedToEvent = true;
        }
        else if(!_IsPromptShowing && HasSubscribedToEvent)
        {
            EventManager.OnSingAction1 -= PressedSingAction1;
            EventManager.OnSingAction2 -= PressedSingAction2;
            EventManager.OnSingAction3 -= PressedSingAction3;
            EventManager.OnSingAction4 -= PressedSingAction4;
            HasSubscribedToEvent = false;
        }

        // now we check if player's input matches prompts
        HasPlayerCompletedAllPrompt = HandleMatchingPrompt();
        if(HasPlayerCompletedAllPrompt && KidObject != null)
        {
            KidObject.IncreaseAngerValue(true);
        }

        if(GameManager.Instance.IsGameEnded())
        {
            // clear all current prompts and reset stuff
            for (int i = 0; i < PromptList.Count; i++)
            {
                if (PromptList[i] != null)
                {
                    PromptList[i].SetActive(false);
                }
            }
        }
    }

    /**********************************************************************/
    private void OnDisable()
    {
    
        PromptList.Clear();
    }

    /**********************************************************************/
    private bool HandleMatchingPrompt()
    {      
        if (_IsPromptShowing && CurrentProcessingActionIndex < PromptCount)
        {
            // if player's input matches current prompt
            if(PlayerInputNoteIndex == PromptList[CurrentProcessingActionIndex].GetComponent<PromptNote>().GetNoteIndex() && HasPlayerSing)
            {
                NoteAnimator = PromptList[CurrentProcessingActionIndex].GetComponent<PromptNote>().GetComponent<Animator>();
                NoteAnimator.SetBool("IsCorrect", true);
                
                //PromptList[CurrentProcessingActionIndex].SetActive(false);

                CurrentProcessingActionIndex++;
                HasPlayerSing = false;
                if(KidObject !=null)
                {
                    KidObject.IncreaseAngerValue();
                }
            }

            if(CurrentProcessingActionIndex >= PromptCount)
            {
                return true;
            }
        }

        
        return false;
    }

    /**********************************************************************/
    private void PressedSingAction1()
    {
        if (!DuckObject.HasDuckActuallySung()) return;
        //PlayerInputNoteIndex = 1;
        HasPlayerSing = true;
        Debug.Log("prompt 1");
    }

    /**********************************************************************/
    private void PressedSingAction2()
    {
        if (!DuckObject.HasDuckActuallySung()) return;
        //PlayerInputNoteIndex = 2;
        HasPlayerSing = true;
        Debug.Log("prompt 2");
    }

    /**********************************************************************/
    private void PressedSingAction3()
    {
        if (!DuckObject.HasDuckActuallySung()) return;
        //PlayerInputNoteIndex = 3;
        HasPlayerSing = true;
        Debug.Log("prompt 3");
    }

    /**********************************************************************/
    private void PressedSingAction4()
    {
        if (!DuckObject.HasDuckActuallySung()) return;
       // PlayerInputNoteIndex = 4;
        HasPlayerSing = true;
        Debug.Log("prompt 4");
    }

    /**********************************************************************/
    public bool IsPromptShowing()
    {
        return _IsPromptShowing;
    }

    /**********************************************************************/
    public void SetPromptNoteIndex(int Index)
    {
        PlayerInputNoteIndex = Index;
    }


}
