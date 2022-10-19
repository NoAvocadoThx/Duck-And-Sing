using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ANGER_LEVEL { NONE, LOW, MEDIUM, HIGH, FEVER };

public class Kid : MonoBehaviour
{

    // serialize fields
    [SerializeField][Range(0, 100)] private int AngerValue = 0;
    [SerializeField] private int AngerIncreaseValue = 2;
    [SerializeField] private int AngerDecreaseValue = 5;

    // public 
    public List<GameObject> GarbageObjectPool;
    public GameObject DuckObject;

    public GameObject AngrySymbol1;
    public GameObject AngrySymbol2;
    public GameObject AngrySymbol3;
    public GameObject AngrySymbol4;

    public AudioSource ThrowAudio;
    // private 
    private float ThrowTimer;
    private bool ShouldThrow;

    private int GarbageIndex;
    private int GarbageCount;

    internal ANGER_LEVEL KidAngryLevel;
    // Start is called before the first frame update
    /**********************************************************************/
    void Start()
    {
        AngerValue = 0;
        ThrowTimer = Random.Range(1.0f, 5.0f);

        GarbageIndex = 0;
        GarbageCount = GarbageObjectPool.Count;
        KidAngryLevel = ANGER_LEVEL.NONE;

        AngrySymbol1.SetActive(false);
        AngrySymbol2.SetActive(false);
        AngrySymbol3.SetActive(false);
        AngrySymbol4.SetActive(false);
    }

    // Update is called once per frame
    /**********************************************************************/
    void Update()
    {
        // set different level of anger
        if (AngerValue <= 0)
        {
            KidAngryLevel = ANGER_LEVEL.NONE;
        }
        else if (AngerValue <= 33 && AngerValue > 0)
        {
            KidAngryLevel = ANGER_LEVEL.LOW;
        }
        else if (AngerValue <= 56 && AngerValue > 33)
        {
            KidAngryLevel = ANGER_LEVEL.MEDIUM;
        }
        else if (AngerValue <= 85 && AngerValue > 56)
        {
            KidAngryLevel = ANGER_LEVEL.HIGH;
        }
        else
        {
            KidAngryLevel = ANGER_LEVEL.FEVER;
        }

        if (KidAngryLevel == ANGER_LEVEL.NONE) return;

        ThrowTimer -= Time.deltaTime;
        if(ThrowTimer <=0)
        {
            ShouldThrow = true;
            ThrowTimer = Random.Range(1.0f, 5.0f);
        }

        if(ShouldThrow && AngerValue > 0)
        {
            int IndexCount = 0;
            GarbageIndex = Random.Range(0, GarbageCount);
            // we find a garbage that matches the anger value
            while (true)
            {            
                
                IndexCount++;
                if(GarbageObjectPool[GarbageIndex].GetComponent<Garbage>().GetGarbageAngerValue() == KidAngryLevel)
                {
                    IndexCount = 0;
                    break;
                }
                else
                {
                    GarbageIndex++;
                    if (GarbageIndex > GarbageCount - 1)
                    {
                        GarbageIndex = 0;
                    }
                }


                if(IndexCount >= GarbageCount)
                {
                    Debug.LogError("We cannot find valid garbage to throw");
                    GarbageIndex = 0;
                    IndexCount = 0;
                    break;
                }
            }

            ThrowGarbage(GarbageIndex);
            ShouldThrow = false;
        }

        HandleAngrySymbols();

        if (AngerValue >= 100)
        {
            GameManager.Instance.GameEnd();
        }
    }
    /**********************************************************************/
    private void HandleAngrySymbols()
    {
        switch (KidAngryLevel)
        {
            case ANGER_LEVEL.NONE:
                break;
            case ANGER_LEVEL.LOW:
                AngrySymbol1.SetActive(true);
                AngrySymbol2.SetActive(false);
                AngrySymbol3.SetActive(false);
                AngrySymbol4.SetActive(false);
                break;
            case ANGER_LEVEL.MEDIUM:
                AngrySymbol1.SetActive(true);
                AngrySymbol2.SetActive(true);
                AngrySymbol3.SetActive(false);
                AngrySymbol4.SetActive(false);
                break;
            case ANGER_LEVEL.HIGH:
                AngrySymbol1.SetActive(true);
                AngrySymbol2.SetActive(true);
                AngrySymbol3.SetActive(true);
                AngrySymbol4.SetActive(false);
                break;
            case ANGER_LEVEL.FEVER:
                AngrySymbol1.SetActive(true);
                AngrySymbol2.SetActive(true);
                AngrySymbol3.SetActive(true);
                AngrySymbol4.SetActive(true);
                break;
                default:
                break;        
        }    
            
       
    }
    /**********************************************************************/
    public void IncreaseAngerValue(bool HasBonus = false)
    {
        if(HasBonus)
        {
            Debug.Log("Bonus Increase");
        }
        AngerValue += HasBonus ? 2 * AngerIncreaseValue : AngerIncreaseValue;
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
    public void ThrowGarbage(int Index)
    {
        Instantiate(GarbageObjectPool[Index], transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 180f));
        GarbageObjectPool[Index].GetComponent<Garbage>().Setup(gameObject, DuckObject, DuckObject.GetComponent<Duck>());
        ThrowAudio.Play();
    }
}
