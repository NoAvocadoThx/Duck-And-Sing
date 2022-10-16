using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Duck : MonoBehaviour
{
    enum CHARACTER_STATE
    {
        NONE, IDLE, SING, DUCK, HIT
    }


    [SerializeField] private float StunDuration = 1f;

    // public
    public Animator DuckAnimator;
    public Kid KidObject;
    public Prompt PromptObject;
    public GameObject SingIcon1;
    public GameObject SingIcon2;
    public GameObject SingIcon3;
    public GameObject SingIcon4;

    public AudioClip Sing1Clip;
    public AudioClip Sing2Clip;
    public AudioClip Sing3Clip;
    public AudioClip Sing4Clip;

    public float Volumn;

    // private
    private CHARACTER_STATE State = CHARACTER_STATE.NONE;
    private SpriteRenderer DuckSprite;
    private float DuckAnimationLength = 0;
    private float DuckStunTimer = 0;
    private bool HasDuckSung = false;
 

    private const int IdleAnimationIndex = 0;
    private const int DuckAnimationIndex = 1;

    private AudioSource Audio;


    // Start is called before the first frame update
    /**********************************************************************/
    void Start()
    {
        Reset();

        DuckAnimationLength = DuckAnimator.runtimeAnimatorController.animationClips[DuckAnimationIndex].length; // "Duck" animation
        Debug.Log("Duck animation length :" + DuckAnimationLength);

        DuckSprite = GetComponent<SpriteRenderer>();

        DuckStunTimer = StunDuration;

        SingIcon1.SetActive(false);
        SingIcon2.SetActive(false);
        SingIcon3.SetActive(false);
        SingIcon4.SetActive(false);

        State = CHARACTER_STATE.IDLE;

        EventManager.OnSingAction1 += SetIsDuckSinging1;
        EventManager.OnSingAction2 += SetIsDuckSinging2;
        EventManager.OnSingAction3 += SetIsDuckSinging3;
        EventManager.OnSingAction4 += SetIsDuckSinging4;

        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    /**********************************************************************/
    void Update()
    {
       

        switch (State)
        {
            case CHARACTER_STATE.NONE:
                {
                    break;
                }
            case CHARACTER_STATE.IDLE:
                {
                    // only handle input if we are in idle state
                    HandleInput();
                    break;
                }
            case CHARACTER_STATE.SING:
                {
                    DuckAnimator.SetBool("IsSinging", true);
                               
                    break;
                }
            case CHARACTER_STATE.DUCK:
                {
                    if(DuckAnimator.GetCurrentAnimatorStateInfo(0).IsName("Duck") && DuckAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        State = CHARACTER_STATE.IDLE;
                    }
                    break;
                }
            case CHARACTER_STATE.HIT:
                {

                    DuckAnimator.SetBool("IsHit", true);
                    DuckStunTimer -= Time.deltaTime;
                    if(DuckStunTimer <=0)
                    {                     
                        DuckStunTimer = StunDuration;
                        State = CHARACTER_STATE.IDLE;
                    }
                    
                    break;
                }
            default:
                break;
               
        }

        // handle stuff related to in/out of certain states
        HandleStateSwitches();



    }
    /**********************************************************************/
    private void HandleStateSwitches()
    {

        DuckAnimator.SetBool("IsDucking", GetIsDucking());

        if (State != CHARACTER_STATE.SING)
        {
            SingIcon1.SetActive(false);
            SingIcon2.SetActive(false);
            SingIcon3.SetActive(false);
            SingIcon4.SetActive(false);
            DuckAnimator.SetBool("IsSinging", false);
            
        }

        if (State == CHARACTER_STATE.HIT)
        {
        }
        else
        {
            DuckAnimator.SetBool("IsHit", false);
        }

        if (State == CHARACTER_STATE.DUCK)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    /**********************************************************************/
    private void HandleInput()
    {
        if (Input.GetButtonDown("Duck"))
        {
            State = CHARACTER_STATE.DUCK;
            return;
        }
    }

    /**********************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Garbage")
        {
            State = CHARACTER_STATE.HIT;
            // Blink twice
            // Should do it with curve
           /* Invoke("EnableBlink", 0f);
            Invoke("DisableBlink", 0.1f);
            Invoke("EnableBlink", 0.2f);
            Invoke("DisableBlink", 0.3f);*/

            Debug.Log("Player got hit");
        }

        KidObject.DecreaseAngerValue();
    }
    /**********************************************************************/
    private void SetIsDuckSinging1()
    {
        if (State == CHARACTER_STATE.SING) return; // if we are already in sing state, we can't sing
        State = CHARACTER_STATE.SING;
        SingIcon1.SetActive(true);
        Audio.PlayOneShot(Sing1Clip, Volumn);
        if (PromptObject != null) PromptObject.SetPromptNoteIndex(1);
        Debug.Log("Duck set to SING");
        HasDuckSung = true;
    }
    /**********************************************************************/
    private void SetIsDuckSinging2()
    {
        if (State == CHARACTER_STATE.SING) return;
        State = CHARACTER_STATE.SING;
        SingIcon2.SetActive(true);
        Audio.PlayOneShot(Sing2Clip, Volumn);
        if (PromptObject != null) PromptObject.SetPromptNoteIndex(2);
        Debug.Log("Duck set to SING");
        HasDuckSung = true;
    }
    /**********************************************************************/
    private void SetIsDuckSinging3()
    {
        if (State == CHARACTER_STATE.SING) return;
        State = CHARACTER_STATE.SING;
        SingIcon3.SetActive(true);
        Audio.PlayOneShot(Sing3Clip, Volumn);
        if (PromptObject != null) PromptObject.SetPromptNoteIndex(3);
        Debug.Log("Duck set to SING");
        HasDuckSung = true;
    }
    /**********************************************************************/
    private void SetIsDuckSinging4()
    {
        if (State == CHARACTER_STATE.SING) return;
        State = CHARACTER_STATE.SING;
        SingIcon4.SetActive(true);
        Audio.PlayOneShot(Sing4Clip, Volumn);
        if (PromptObject != null) PromptObject.SetPromptNoteIndex(4);
        Debug.Log("Duck set to SING");
        HasDuckSung = true;
    }

    /**********************************************************************/
    private void OnDisable()
    {

        EventManager.OnSingAction1 -= SetIsDuckSinging1;
        EventManager.OnSingAction2 -= SetIsDuckSinging2;
        EventManager.OnSingAction3 -= SetIsDuckSinging3;
        EventManager.OnSingAction4 -= SetIsDuckSinging4;
    }
    /**********************************************************************/
    private void Reset()
    {
        State = CHARACTER_STATE.NONE;
        DuckAnimationLength = 0;

    }

    /**********************************************************************/
    private void EnableBlink()
    {
        DuckSprite.color = new Color(1, 0, 0, 1);
    }

    /**********************************************************************/
    private void DisableBlink()
    {
        DuckSprite.color = new Color(1, 1, 1, 1);
    }

    /**********************************************************************/
    public bool GetIsDucking()
    {
        return State == CHARACTER_STATE.DUCK;
    }

    /**********************************************************************/
    public bool GetIsSinging()
    {
        return State == CHARACTER_STATE.SING;
    }

    /**********************************************************************/
    public void DoneSinging()
    {
        if (HasDuckSung)
        {
            State = CHARACTER_STATE.IDLE;
            HasDuckSung = false;
        }
    }

    /**********************************************************************/
    public bool HasDuckActuallySung()
    {
        return HasDuckSung;
    }

   
}
