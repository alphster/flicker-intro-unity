using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance { get { return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>(); } }

    Vector3 StartPosition = new Vector3(0f, .8f, -5.3f);
    Vector3 StartRotation = new Vector3(2.8f, 0, 0);

    Vector3 IntroPosition = new Vector3(0f, 3f, -6f);
    Vector3 IntroRotation = new Vector3(20f, 0, 0);

    Vector3 FirePosition = new Vector3(0f, 10.25f, -12f);
    Vector3 FireRotation = new Vector3(38f, 0, 0);

    // Use this for initialization
    void Start()
    {
        this.transform.position = StartPosition;
        this.transform.eulerAngles = StartRotation;
    }

    public void MoveToIntroPosition()
    {
        StartCoroutine(Transition(
            IntroPosition,
            IntroRotation, 
            4f));
    }

    public void MoveToFirePosition()
    {
        StartCoroutine(Transition(
            FirePosition,
            FireRotation,
            4f));
    }
        
    IEnumerator Transition(Vector3 ePos, Vector3 eRot, float time)
    {
        float t = 0.0f;
        var sPos = transform.position;
        var sRot = transform.eulerAngles;
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