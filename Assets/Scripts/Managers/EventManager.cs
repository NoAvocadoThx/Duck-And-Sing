using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void SingAction();
    public static event SingAction OnSingAction1;
    public static event SingAction OnSingAction2;
    public static event SingAction OnSingAction3;
    public static event SingAction OnSingAction4;


    private void Update()
    {
        if (Input.GetButtonDown("Sing1"))
        {
            if (OnSingAction1 != null)
            {
                OnSingAction1();
                Debug.Log("Pressed sing 1");
            }

        }
        if (Input.GetButtonDown("Sing2"))
        {
            if(OnSingAction2 != null)
            { 
                OnSingAction2();
                Debug.Log("Pressed sing 2");
            }
           
        }
        if (Input.GetButtonDown("Sing3"))
        {
           if(OnSingAction3!=null)
           {
                OnSingAction3();
                Debug.Log("Pressed sing 3");
           }
            
        }
        if (Input.GetButtonDown("Sing4"))
        {
            if(OnSingAction4 != null)
            {
                OnSingAction4();
                Debug.Log("Pressed sing 4");
            }
            
        }
    }
}
