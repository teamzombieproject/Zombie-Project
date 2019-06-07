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
    public bool hasSupplyDropSpawned = false;
    public bool canZombiesSpawn = false;

    private float m_GameTime = 0f;
    public float GameTime { get { return m_GameTime; } }
    public float spawnDeactivate = 30f;
    public float spawnTimer = 0f;

    public Health playerHealth;
    public ZombieSpawner spawns;

    public GameObject playerObject;
    public GameObject currentPlayer;
    public GameObject cameraRigObject;
    public GameObject radioObject;
    public GameObject playerSpawn;
    public GameObject radioSpawn;
    public GameObject supplyDropObject;
    public GameObject supplyDropSpawn;
    public GameObject currentSupplyDrop;

    public int gameScore = 0;
    public int wave = 0;
    public int zombiesAlive = 0;
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
        Destroy(GameObject.FindGameObjectWithTag("Player"));                            // remove old player object
        Destroy(GameObject.FindGameObjectWithTag("CameraRig"));
        Destroy(GameObject.FindGameObjectWithTag("Radio"));                             // remove old radio object

        // destroy all spawnable game objects that may remain from previous game
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");             // find all of the zombies that are left over from the last game and store them in an array
        GameObject[] Traps = GameObject.FindGameObjectsWithTag("Traps");
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        //GameObject[] Turrets = GameObject.FindGameObjectsWithTag("Turret");
        
        foreach(GameObject z in zombies)
        {
            Destroy(z);                                                                 // then loop through our array and destroy them all
        }

        foreach(GameObject t in Traps)
        {
            Destroy(t);
        }

        foreach(GameObject b in barricades)
        {
            Destroy(b);
        }

        // foreach (GameObject tt in Turret)
        // {
        //     Destroy(tt);
        // }

        // spawn new player object
        currentPlayer = Instantiate(playerObject, playerSpawn.transform.position, playerSpawn.transform.rotation);
        Instantiate(cameraRigObject);
        Instantiate(radioObject, radioSpawn.transform);

        playerHealth = currentPlayer.GetComponent<Health>();

        // reset variables (score, wave number, resources, etc.)
        wave = 0;
        gameScore = 0;
        zombiesAlive = 0;
        mineStock = 0;
        bearTrapStock = 0;
        barricadeStock = 0;
        machineGunTurretStock = 0;
        javelinRocketTurretStock = 0;

        // change to the action state once everything is setup)
        m_GameState = GameState.Action;
    }

    void SpawnSupplyDrop()
    {
        if (currentSupplyDrop == null)
        {
            currentSupplyDrop = Instantiate(supplyDropObject, supplyDropSpawn.transform.position, supplyDropSpawn.transform.rotation);
            hasSupplyDropSpawned = true;
        }

    }

    void CheckState()
    {
        switch (m_GameState)
        {
            case GameState.Menu:
                if (playButtonPressed == true)
                {
                    m_GameState = GameState.Setup;
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
                spawnTimer += Time.deltaTime;

                if (playerHealth.deathIsFinished == true)
                {
                    m_GameState = GameState.GameOver;
                }

                // Make the supply drop spawn (set Spawn bool to true) (can only spawn when false)
                if (!hasSupplyDropSpawned)
                {
                    //spawnSupplyDrop()
                }
                // change hud
                if ( spawnTimer >= spawnDeactivate)
                {
                    // spawns.maySpawn = false;
                    canZombiesSpawn = false;
                }

                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                }

                // All zombies dead change to build state
                if (!canZombiesSpawn && zombiesAlive == 0)
                {
                    m_GameState = GameState.Build;
                }

                break;
            case GameState.Build:
                m_GameTime += Time.deltaTime;

                // change hud to building hud

                if (m_GameTime >= 30)
                {
                    m_GameState = GameState.Action;
                    m_GameTime = 0f;
                    // spawns.maySpawn = true;
                    canZombiesSpawn = true;
                    spawnTimer = 0f;
                    hasSupplyDropSpawned = false;
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
