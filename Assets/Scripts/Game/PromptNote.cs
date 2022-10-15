using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PromptNote : MonoBehaviour
{
    [SerializeField] private int _NoteIndex;
    [SerializeField] private float MAX_SCALE = 0.6f;
    [SerializeField] private float START_SCALE = 0.1f;
    [SerializeField] private float SCALE_TIME = 0.1f;


    public AnimationCurve FloatingCuve;
    [SerializeField] private float FlotingSpeed = 0.6f;

    private float ScaleVal;
    private float InitialYPosition;
    private Kid KidObject;

    int NoteIndex { get { return _NoteIndex; }  }

    /**********************************************************************/
    public int GetNoteIndex()
    {
        return NoteIndex;
    }
    /**********************************************************************/
    public void OnCorrectPromptAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    /**********************************************************************/
    private void Start()
    {
        
        ScaleVal = START_SCALE;
        FlotingSpeed = Random.Range(0.55f, 0.65f);
        InitialYPosition = transform.position.y;
        KidObject = FindObjectOfType<Kid>();
        StartCoroutine(ScaleNote(MAX_SCALE, SCALE_TIME));
    }

    /**********************************************************************/
    private void Update()
    {
        float WeightedFloatingSpeed = FlotingSpeed * ( (float)KidObject.GetAngerValue()/ 100.0f + 1);
        transform.position = new Vector3(transform.position.x, FloatingCuve.Evaluate(((Time.time * WeightedFloatingSpeed) % FloatingCuve.length)) + InitialYPosition, transform.position.z);
    }



    /**********************************************************************/
    IEnumerator ScaleNote(float EndScaleValue, float Duration)
    {
        float time = 0;
        float startValue = ScaleVal;
        while (time < Duration)
        {
            float t = time / Duration;
            t = t * t * (3f - 2f * t); // smooth lerping function
            ScaleVal = Mathf.Lerp(startValue, EndScaleValue, t);
            gameObject.transform.localScale = new Vector3(ScaleVal, ScaleVal, ScaleVal);

            time += Time.deltaTime;
            yield return null;
        }
        ScaleVal = EndScaleValue;
    }
}
