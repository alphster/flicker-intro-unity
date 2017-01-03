using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get { return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>(); } }

    GameState currentGameState;
    bool listenToFireClicks = false;

    void Awake()
    {
        currentGameState = GameState.Start;
    }

    // Use this for initialization
    void Start()
    {
        StartMenuCanvas.Instance.DisableAll();
        Campfire.Instance.ChangeFire(FireSize.Embers);
        //StartMenuCanvas.Instance.FadeInAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireClickListener()
    {        
        if (currentGameState == GameState.Start)
            SetState(GameState.Title);
        else if (currentGameState == GameState.Title)
            SetState(GameState.Intro);
        else if (currentGameState == GameState.Intro)
            SetState(GameState.Campfire);
    }

    public void SetState(GameState state)
    {
        if (state == GameState.Title)
        {            
            StartCoroutine(TransitionToTitleState());
        }
        else if (state == GameState.Intro)
        {
            StartCoroutine(TransitionToIntroState());
        }
        else if (state == GameState.Campfire)
        {
            StartCoroutine(TransitionToCampfireState());
            currentGameState = state;
        }

        currentGameState = state;
    }

    IEnumerator TransitionToTitleState()
    {
        Campfire.Instance.ClickEnabled = false;
        yield return StartCoroutine(StartMenuCanvas.Instance.FadeInAll());
        Campfire.Instance.ClickEnabled = true;
    }

    IEnumerator TransitionToIntroState()
    {
        Campfire.Instance.ClickEnabled = false;
        yield return StartCoroutine(StartMenuCanvas.Instance.FadeOutAll());
        yield return StartCoroutine(MainCamera.Instance.MoveToIntroPosition());
        MainCanvas.Instance.PlayTextGroup("intro");
        Campfire.Instance.ClickEnabled = true;
    }

    IEnumerator TransitionToCampfireState()
    {
        Campfire.Instance.ClickEnabled = false;
        yield return StartCoroutine(MainCamera.Instance.MoveToFirePosition());
        Campfire.Instance.ClickEnabled = true;
    }
}

public enum GameState
{
    Start,
    Title,
    Intro,
    Campfire
}
