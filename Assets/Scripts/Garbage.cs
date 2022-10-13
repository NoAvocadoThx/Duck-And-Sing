using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class Garbage : MonoBehaviour
{
    // public
    public GameObject Origin;
    public GameObject Target;

    public Duck DuckObject;

    [SerializeField] private float ThrowSpeed = 6.0f;

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
        ThrowSpeed = Random.Range(6f, 10f);
    }

    /**********************************************************************/
    // Update is called once per frame
    void Update()
    {
        if (Origin == null || Target == null) return;
        OriginX = Origin.transform.position.x;
        TargetX = Target.transform.position.x;

        DistanceBetweenOriginAndTarget = OriginX - TargetX;

        // calculate x position for next frame
        NextFrameX = Mathf.MoveTowards(transform.position.x, TargetX, ThrowSpeed * Time.deltaTime);
        // calculate y position of current frame
        BaseY = Mathf.Lerp(Origin.transform.position.y, Origin.transform.position.y, (NextFrameX - OriginX) / DistanceBetweenOriginAndTarget);
        // initial height
        Height = 2 * (NextFrameX - OriginX) * (NextFrameX - TargetX) / (-0.25f * DistanceBetweenOriginAndTarget * DistanceBetweenOriginAndTarget);

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
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }

    /**********************************************************************/
    public void Setup(GameObject Origin_In, GameObject Target_In, Duck Duck_In)
    {
        Origin = Origin_In;
        Target = Target_In;
        DuckObject = Duck_In;
    }

}
