using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerBar : MonoBehaviour
{

    public Kid KidObject;
    public GameObject Filler;


    private Slider AngerSlider;
    private int AngerVal;

    /**********************************************************************/
    private void Awake()
    {
        AngerSlider = gameObject.GetComponent<Slider>();
    }

    /**********************************************************************/
    // Start is called before the first frame update
    void Start()
    {
        Filler.SetActive(false);
    }

    /**********************************************************************/
    // Update is called once per frame
    void Update()
    {
        if (KidObject != null)
        {
            AngerVal= KidObject.GetAngerValue();
        }
        if(AngerVal>0)
        {
            Filler.SetActive(true);
        }
        AngerSlider.value = AngerVal;
    }
}
