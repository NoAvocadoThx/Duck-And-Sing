using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    // public
    public GameObject Origin;
    public GameObject Target;

    public Duck DuckObject;
    public ANGER_LEVEL AngerLevel;
    public bool IsBolt = false;

    [SerializeField] private Beat B;
    [SerializeField] private float ThrowSpeed = 6.0f;
    [SerializeField] private float ThrowHeightFactor = 2.0f;
    // private
    private float OriginX;
    private float TargetX;

    private float DistanceBetweenOriginAndTarget;
    private float NextFrameX;
    private float BaseY;
    private float Height;
    
    private Vector3 MovePosition;

    /**********************************************************************/
    // Start is called before the first frame update
    void Start()
    {
        B = FindObjectOfType<Beat>();
        if (B != null)
        {
            ThrowSpeed = Random.Range(6f * B.beat, 10f * B.beat);
            ThrowSpeed = Mathf.Min(6, ThrowSpeed);
        }

        ThrowHeightFactor = Random.Range(1.0f, 4.0f);
        if (Origin != null)
        {
            Height = Origin.transform.position.y;
        }
    }

    /**********************************************************************/
    // Update is called once per frame
    void Update()
    {
        if (Origin == null || Target == null) Destroy(gameObject);
        OriginX = Origin.transform.position.x;
        TargetX = Target.transform.position.x;

       

        DistanceBetweenOriginAndTarget = OriginX - TargetX;

        // calculate x position for next frame
        NextFrameX = Mathf.MoveTowards(transform.position.x, TargetX, ThrowSpeed * Time.deltaTime);
        if (!IsBolt)
        {
            // calculate y position of current frame
            BaseY = Mathf.Lerp(Origin.transform.position.y, Origin.transform.position.y, (NextFrameX - OriginX) / DistanceBetweenOriginAndTarget);
            // initial height
            Height = ThrowHeightFactor * (NextFrameX - OriginX) * (NextFrameX - TargetX) / (-0.25f * DistanceBetweenOriginAndTarget * DistanceBetweenOriginAndTarget);
        }
        MovePosition = new Vector3(NextFrameX, BaseY + Height, transform.position.z);
        transform.rotation = LookAtTarget(MovePosition - transform.position);
        transform.position = MovePosition;

        // we hit the duck
        if (MovePosition.x <= Target.transform.position.x)
        {
            Destroy(gameObject);

            // TODO
            if (DuckObject != null && DuckObject.GetIsDucking())
            {

            }
        }

        

    }

    /**********************************************************************/
    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 180, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }

    /**********************************************************************/
    public void Setup(GameObject Origin_In, GameObject Target_In, Duck Duck_In)
    {
        Origin = Origin_In;
        Target = Target_In;
        DuckObject = Duck_In;
      
    }

    /**********************************************************************/
    public ANGER_LEVEL GetGarbageAngerValue()
    {
        return AngerLevel;
    }

}
