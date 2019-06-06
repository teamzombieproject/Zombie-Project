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

    public GameObject playerObject;
    public GameObject cameraRigObject;
    public GameObject radioObject;
    public GameObject playerSpawn;
    public GameObject radioSpawn;

    public int gameScore = 0;
    public int wave = 0;
    public int machineGunTurretStock = 0;
    public int javelinRocketTurretStock = 0;
    public int mineStock = 0;
    public int bearTrapStock = 0;
    public int barricadeStock = 0;

    public enum GameState
    {
        Menu,
        Action,
        Setup,
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
        m_GameState = GameState.Setup;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    void Init()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Player"));                            // remove old player object
        //Destroy(GameObject.FindGameObjectWithTag("CameraRig"));
        Destroy(GameObject.FindGameObjectWithTag("Radio"));                             // remove old radio object

        // destroy all spawnable game objects that may remain from previous game
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");             // find all of the zombies that are left over from the last game and store them in an array
        GameObject[] zombieSpawnPoints = GameObject.FindGameObjectsWithTag("ZombieSpawnePoint");
        GameObject[] bearTraps = GameObject.FindGameObjectsWithTag("BearTrap");
        GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        //GameObject[] machineGunTurret = GameObject.FindGameObjectsWithTag("???");
        //GameObject[] javelinRocketTurret = GameObject.FindGameObjectsWithTag("???");
        
        foreach(GameObject z in zombies)
        {
            Destroy(z);                                                                 // then loop through our array and destroy them all
        }

        foreach(GameObject bt in bearTraps)
        {
            Destroy(bt);
        }

        foreach(GameObject m in mines)
        {
            Destroy(m);
        }

        foreach(GameObject b in barricades)
        {
            Destroy(b);
        }

        foreach(GameObject zsp in zombieSpawnPoints)
        {
            
        }

        foreach (GameObject m in mines)
        {
            Destroy(m);
        }

        // foreach (GameObject mt in ???)
        // {
        //     Destroy(mt);
        // }

        // foreach (GameObject rt in ???)
        // {
        //     Destroy(rt);
        // }

        // spawn new player object
        //Instantiate(playerObject, playerSpawn.transform);
        //Instantiate(cameraRigObject);
        Instantiate(radioObject, radioSpawn.transform);


        // reset variables (score, wave number, resources, etc.)
        wave = 0;
        gameScore = 0;
        mineStock = 0;
        bearTrapStock = 0;
        barricadeStock = 0;
        machineGunTurretStock = 0;
        javelinRocketTurretStock = 0;

        // change to the action state once everything is setup)
        m_GameState = GameState.Action;
    }

    void CheckState()
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
            case GameState.Setup:
                Init();
                break;
            case GameState.Action:
                bool isGameOver = false;

                //   if (playerDead == true || broadcastEquipmentDestroyed == true)
                //  {
                //        (Timer if I can make it) (or have a getButtonDown thing that activates it)
                //        ("Game over" or "you're dead" written on the screen for a certain time (hense the timer))
                //        m_GameState = GameState.Menu;
                // }

                // change hud
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

                if (m_GameTime >= 30)
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
