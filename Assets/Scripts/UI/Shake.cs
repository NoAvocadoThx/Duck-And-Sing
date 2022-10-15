using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //House transform and anger variables
    [SerializeField] private GameObject House;
    private Vector3 OriginalPos;
    private AngerBar anger;

    //Shaking Variables
    private float ShakingValue;
    private bool shaking = false;


    private void Start()
    {
        anger = GetComponent<AngerBar>();
        OriginalPos = House.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        ExecuteShakeCoroutine();
        ShakingMethod();

        
    }

    IEnumerator ShakeNow()
    {
        Vector2 originalPos = House.transform.position;

        if(shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.25f);

        shaking = false;
        House.transform.position = originalPos;
    }

    //Starting and stoping the shaking effect based on Anger Value
    void ExecuteShakeCoroutine()
    {
        if (ShakingValue > 0)
        {
            StartCoroutine("ShakeNow");

        }
        else
        {
            StopCoroutine("ShakeNow");
        }
    }

    //Shaking 
    void ShakingMethod()
    {
        ShakingValue = anger.KidObject.GetAngerValue();

        if (shaking == true)
        {
            Vector2 newPos = OriginalPos + Random.insideUnitSphere * (Time.deltaTime * ShakingValue);
            newPos.y = House.transform.position.y;


            House.transform.position = newPos;
        }
    }
}
