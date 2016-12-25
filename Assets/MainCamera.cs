using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    public static MainCamera Instance
    {
        get
        {
            return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        }
    }

    public Vector3 startPosition = new Vector3(0f, .8f, -5.3f);
    public Vector3 startRotation = new Vector3(2.8f, 0, 0);

    public Vector3 firePosition = new Vector3(0f, 10.25f, -12f);
    public Vector3 fireRotation = new Vector3(38f, 0, 0);

    // Use this for initialization
    void Start()
    {
        this.transform.position = startPosition;
        this.transform.eulerAngles = startRotation;
    }

    public void MoveToIntroPosition()
    {
        StartCoroutine(Transition(
            startPosition, 
            startRotation, 
            firePosition, 
            fireRotation, 
            4f));
    }
        
    IEnumerator Transition(Vector3 sPos, Vector3 sRot, Vector3 ePos, Vector3 eRot, float time)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);
            var smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            transform.position = Vector3.Lerp(sPos, ePos, Mathf.SmoothStep(0.0f, 1.0f, smoothT));
            transform.eulerAngles = Vector3.Lerp(sRot, eRot, Mathf.SmoothStep(0.0f, 1.0f, smoothT));
            yield return 0;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}