using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuCanvas : MonoBehaviour {

    public static StartMenuCanvas Instance { get { return GameObject.FindGameObjectWithTag("StartMenuCanvas").GetComponent<StartMenuCanvas>(); } }

    public GameObject Title;
    public GameObject ClickTheFire;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FadeInAll()
    {
        StartCoroutine(FadeInTitleAllAnimation());
    }

    public void FadeOutAll()
    {
        StartCoroutine(FadeOutTitleAnimation());
    }    

    IEnumerator FadeInTitleAllAnimation()
    {
        var time = 3f;
        var invisible = Color.white;
        invisible.a = 0f;

        var titleText = StartMenuCanvas.Instance.Title.GetComponent<Text>();
        var clickTheFireText = StartMenuCanvas.Instance.ClickTheFire.GetComponent<Text>();

        titleText.color = invisible;
        clickTheFireText.color = invisible;

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);
            var smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            var color = Color.white;
            color.a = smoothT;
            titleText.color = color;
            yield return 0;
        }

        t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);
            var smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            var color = Color.white;
            color.a = smoothT;
            clickTheFireText.color = color;
            yield return 0;
        }       
    }

    IEnumerator FadeOutTitleAnimation()
    {
        var time = 3f;

        var titleText = StartMenuCanvas.Instance.Title.GetComponent<Text>();
        var clickTheFireText = StartMenuCanvas.Instance.ClickTheFire.GetComponent<Text>();

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);
            var smoothT = Mathf.SmoothStep(1.0f, 0.0f, t);
            var color = Color.white;
            color.a = smoothT;
            titleText.color = color;
            clickTheFireText.color = color;
            yield return 0;
        }       
    }
}
