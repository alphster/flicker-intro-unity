using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour {

    Dictionary<string, TextDetails[]> textGroups = new Dictionary<string, TextDetails[]> {
        {
        "intro", new TextDetails[]
            {
            new TextDetails(3, "The fire flickers dimly in a dark and open space."),
            new TextDetails(3, "Some more intro text."),
            new TextDetails(3, "Some more intro text.")
            }
        }
    };

    Queue<TextDetails> textQueue = new Queue<TextDetails>();
    
    public static MainCanvas Instance { get { return GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<MainCanvas>(); } }

    Text storyTextOld;
    Text storyTextNew;

    bool isTextFadingIn = false;

    private void Awake()
    {
        storyTextOld = GameObject.FindGameObjectWithTag("StoryTextOld").GetComponent<Text>();
        storyTextNew = GameObject.FindGameObjectWithTag("StoryTextNew").GetComponent<Text>();
    }

    void Start () {
		
	}
	
	void Update () {
        HandleTextQueue();
	}
    
    public void DequeueTextGroup(string groupName)
    {
        foreach (var t in textGroups[groupName])
        {
            textQueue.Enqueue(t);            
        }
    }

    private void HandleTextQueue()
    {
        if (!isTextFadingIn && textQueue.Count > 0)
        {
            isTextFadingIn = true;
            StartCoroutine(FadeInText(textQueue.Dequeue()));
        }
    }

    IEnumerator FadeInText(TextDetails textDetail)
    {
        var time = textDetail.Delay; //* (GameController.Instance.SpeedMode ? .001f : 1);
        var invisible = Color.white;
        invisible.a = 0f;

        storyTextOld.text = storyTextNew.text + Environment.NewLine + storyTextOld.text;
        storyTextNew.text = textDetail.Text;

        storyTextNew.color = invisible;
                
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / .3f);
            var smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            var color = Color.white;
            color.a = smoothT;
            storyTextNew.color = color;
            yield return 0;
        }

        t = 0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);            
            yield return 0;
        }

        isTextFadingIn = false;
    }

    class TextDetails
    {
        public string Text { get; set; }
        public float Delay { get; set; }

        public TextDetails(int delay, string text)
        {
            Text = text;
            Delay = delay;
        }
    }
}
