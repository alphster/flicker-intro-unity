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
        this.transform.position = firePosition;
        this.transform.eulerAngles = fireRotation;
    }


    // Update is called once per frame
    void Update()
    {

    }
}