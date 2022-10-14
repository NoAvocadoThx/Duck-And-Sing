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

    private float ScaleVal;
    int NoteIndex { get { return _NoteIndex; } }

    private void Start()
    {
        
        ScaleVal = START_SCALE;
        StartCoroutine(ScaleNote(MAX_SCALE, SCALE_TIME));
    }

    private void Update()
    {
        
    }

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
