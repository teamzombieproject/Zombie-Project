using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playButtonPressed = false;
    public bool highScoresButtonPressed = false;
    public bool optionsButtonPressed = false;
    public bool creditsButtonPressed = false;
    public bool quitButtonPressed = false;

    private float m_GameTime = 0f;
    public float GameTime { get { return m_GameTime; } }

    public enum GameState
    {
        Menu,
        Action,
        Build,
        HighScores,
        Options,
        Credits,
        GameOver
    }

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Menu;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Menu:
                if (playButtonPressed == true)
                {
                    m_GameState = GameState.Action;
                }
                if (highScoresButtonPressed == true)
                {
                    m_GameState = GameState.HighScores;
                }
                if (optionsButtonPressed == true)
                {
                    m_GameState = GameState.Options;
                }
                if (creditsButtonPressed == true)
                {
                    m_GameState = GameState.Credits;
                }
                if (quitButtonPressed == true)
                {
                    Application.Quit();
                }
                break;
            case GameState.Action:
                bool isGameOver = false;

                    //   if (playerDead == true || broadcastEquipmentDestroyed == true)
              //  {
                   //        (Timer if I can make it) (or have a getButtonDown thing that activates it)
                   //        ("Game over" or "you're dead" written on the screen for a certain time (hense the timer))
                   //        m_GameState = GameState.Menu;
               // }

                // Zombie spawn stuff
                // All zombies dead change to build state

                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                }
                break;
            case GameState.Build:
                m_GameTime += Time.deltaTime;

                // change hud to building hud

                if (m_GameTime <= 30)
                {
                    m_GameState = GameState.Action;
                    m_GameTime = 0f;
                }
                break;
            case GameState.HighScores:
                // if HS_BackButtonPressed
                // {
                // m_GameState = GameState.Menu;
                // }
                break;
            case GameState.Options:



                break;
            case GameState.Credits:



                break;
            case GameState.GameOver:



                break;
        }
    }
}
