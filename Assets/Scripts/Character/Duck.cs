using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Duck : MonoBehaviour
{
    enum CHARACTER_STATE
    {
        NONE, IDLE, SING, DUCK
    }

    // public
    public Animator DuckAnimator;

    // private
    private CHARACTER_STATE State = CHARACTER_STATE.NONE;
    private float DuckAnimationLength = 0;

    private const int IdleAnimationIndex = 0;
    private const int DuckAnimationIndex = 1;

    // SerializedField
    [SerializeField] private int Duck_HP = 5;





    // Start is called before the first frame update
    /**********************************************************************/
    void Start()
    {
        Reset();

        DuckAnimationLength = DuckAnimator.runtimeAnimatorController.animationClips[DuckAnimationIndex].length; // "Duck" animation
        Debug.Log("Duck animation length :" + DuckAnimationLength);

        State = CHARACTER_STATE.IDLE;
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
            default:
                break;
               
        }

        DuckAnimator.SetBool("IsDucking", GetIsDucking());
    }

    /**********************************************************************/
    private void HandleInput()
    {
        if (Input.GetButtonDown("Duck"))
        {
            State = CHARACTER_STATE.DUCK;
           
        }
    }

    /**********************************************************************/
    private void Reset()
    {
        State = CHARACTER_STATE.NONE;
        DuckAnimationLength = 0;

    }

    /**********************************************************************/
    private bool GetIsDucking()
    {
        return State == CHARACTER_STATE.DUCK;
    }
}
