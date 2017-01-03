using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour {

    private Dictionary<string, string[]> textGroups = new Dictionary<string, string[]> {
        {
        "intro", new string[]
            {
            "The fire flickers dimly in a dark and open space.",
            "Some more intro text.",
            "Some more intro text."
            }
        }
    };
    
    public static MainCanvas Instance { get { return GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<MainCanvas>(); } }

    Text storyText;

    private void Awake()
    {
        storyText = GameObject.FindGameObjectWithTag("StoryText").GetComponent<Text>();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
    
    public void PlayTextGroup(string groupName)
    {
        foreach (var t in textGroups[groupName])
        {
            storyText.text = t + Environment.NewLine + storyText.text;
        }
    }     
}
