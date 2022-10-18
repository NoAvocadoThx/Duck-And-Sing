using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrySymbol : MonoBehaviour
{
    public AnimationCurve ScaleCuve;

    private float Timer;

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale= new Vector3(ScaleCuve.Evaluate(((Time.time) % ScaleCuve.length)), ScaleCuve.Evaluate(((Time.time ) % ScaleCuve.length)), ScaleCuve.Evaluate(((Time.time) % ScaleCuve.length)));
    }
}
