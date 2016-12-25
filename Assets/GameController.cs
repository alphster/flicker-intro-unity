using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public static GameController Instance
    {
        get
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    private GameState currentGameState;

    // Use this for initialization
    void Start()
    {
        currentGameState = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetState(GameState state)
    {
        if (state == GameState.Intro)
        {
            if (currentGameState == GameState.Start)
                MainCamera.Instance.MoveToIntroPosition();
        }
        else if (state == GameState.Campfire)
        {
            if (currentGameState == GameState.Intro)
                // something
                currentGameState = state;
        }

        currentGameState = state;
    }

}

public enum GameState
{
    Start,
    Intro,
    Campfire
}
