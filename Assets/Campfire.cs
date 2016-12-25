using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {        
        GameController.Instance.SetState(GameState.Intro);
    }
}
