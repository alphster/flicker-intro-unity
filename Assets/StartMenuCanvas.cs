using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuCanvas : MonoBehaviour {

    public static StartMenuCanvas Instance { get { return GameObject.FindGameObjectWithTag("StartMenuCanvas").GetComponent<StartMenuCanvas>(); } }

    public GameObject Title;
    public GameObject ClickTheFire;

    Text titleText;
    Text clickTheFireText;

    private void Awake()
    {
        titleText = StartMenuCanvas.Instance.Title.GetComponent<Text>();
        clickTheFireText = StartMenuCanvas.Instance.ClickTheFire.GetComponent<Text>();
    }

    void Start () {

    }
	

	void Update () {
	
	}

    public void DisableAll()
    {       
        titleText.enabled = false;
        clickTheFireText.enabled = false;
    }

    public IEnumerator FadeInAll()
    {
        var time = 3f;
        var invisible = Color.white;
        invisible.a = 0f;

        titleText.enabled = true;
        clickTheFireText.enabled = true;

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

    public IEnumerator FadeOutAll()
    {
        var time = 3f;

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

        titleText.enabled = false;
        clickTheFireText.enabled = false;
    }
}
