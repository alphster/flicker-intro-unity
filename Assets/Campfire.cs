using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour {

    public static Campfire Instance { get { return GameObject.FindGameObjectWithTag("Campfire").GetComponent<Campfire>(); } }

    FireSize fireSize = FireSize.Flicker;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {   
        GameController.Instance.SetState(GameState.Intro);
        //if (GameController.Instance.stat)

        //GetComponent<BoxCollider>().enabled = false;
    }

    void ChangeFire(bool increase)
    {
        if (increase && fireSize < FireSize.Roaring)
            fireSize++;

        if (increase && fireSize > FireSize.Embers)
            fireSize--;

        switch (fireSize)
        {
            case FireSize.Embers:
                Debug.Log("Fire is burning embers.");
                
                break;
            case FireSize.Flicker:
                Debug.Log("Fire is flickering.");
                break;                
            case FireSize.Burning:
                Debug.Log("Fire is burning brightly.");
                break;
            case FireSize.Roaring:
                Debug.Log("Fire is roaring.");
                break;
            default:
                Debug.Log("Tried to change the fire into an unknown size.");
                break;
        }
        
    }
}

public enum FireSize
{
    Embers,
    Flicker,
    Burning,
    Roaring
}
