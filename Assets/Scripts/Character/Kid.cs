using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
 
    // serialize fields
    [SerializeField][Range(0, 100)] private int AngerValue = 0;
    [SerializeField] private int AngerIncreaseValue = 2;
    [SerializeField] private int AngerDecreaseValue = 5;

    // public 
    public GameObject GarbageObject;
    public GameObject DuckObject;
    public Beat B;

    // private 
    private float ThrowTimer;
    private bool ShouldThrow;
  
    // Start is called before the first frame update
    /**********************************************************************/
    void Start()
    {
        AngerValue = 0;
        ThrowTimer = Random.Range(1.0f, 5.0f);


    }

    // Update is called once per frame
    /**********************************************************************/
    void Update()
    {
        ThrowTimer -= Time.deltaTime;
        if(ThrowTimer <=0)
        {
            ShouldThrow = true;
            ThrowTimer = Random.Range(1.0f, 5.0f);
        }

        if(ShouldThrow && AngerValue > 0)
        {
            ThrowGarbage();
            ShouldThrow = false;
        }
    }

    /**********************************************************************/
    public void IncreaseAngerValue()
    {
        AngerValue += AngerIncreaseValue;
        AngerValue = Mathf.Min(AngerValue, 100);
    }

    /**********************************************************************/
    public void DecreaseAngerValue()
    {
        AngerValue -= AngerDecreaseValue;
        AngerValue = Mathf.Max(0, AngerValue);
    }
    /**********************************************************************/
    public int GetAngerValue()
    {
        return AngerValue;
    }

    /**********************************************************************/
    public void ThrowGarbage()
    {
        Instantiate(GarbageObject, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
        GarbageObject.GetComponent<Garbage>().Setup(gameObject, DuckObject, DuckObject.GetComponent<Duck>(), B.GetComponent<Beat>());
    }
}
