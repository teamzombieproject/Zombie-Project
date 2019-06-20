using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool deBugMod = false;

    public bool playButtonPressed = false;
    public bool highScoresButtonPressed = false;
    public bool optionsButtonPressed = false;
    public bool creditsButtonPressed = false;
    public bool quitButtonPressed = false;
    public bool hasSupplyDropSpawned = false;
    public bool hasBEDropSpawned = false;
    public bool bEPiecePickedUp = false;
    public bool canGunBeSpawned = true;
    public bool canZombiesSpawn = false;
    public bool actionPhaseActive = false;
    public bool isRadioDead = false;
   

    public float m_GameTime = 0f;
    public float GameTime { get { return m_GameTime; } }
    public float spawnDeactivate = 30f;
    public float spawnTimer = 0f;
    public float bEDropTimeEnd = 20f;

    public Health playerHealth;
    public ZombieSpawner spawns;

    public GameObject supplyDropSpawn;
    public GameObject bEDropSpawn;
    public GameObject weaponDropSpawn;
    public GameObject playerSpawn;
    public GameObject radioSpawn;
    public GameObject zombieSpawner;
    public GameObject supplyDropObject;
    public GameObject bEDropObject;
    public GameObject weaponDropObject;
    public GameObject playerObject;
    public GameObject radioObject;
    public GameObject cameraRigObject;
    public GameObject currentSupplyDrop;
    public GameObject currentBEDrop;
    public GameObject currentWeaponDrop;
    public GameObject currentPlayer;
    public GameObject mineDrop;
    public GameObject bearTrapDrop;
    public GameObject machineGunTurretDrop;
    public GameObject javelinRocketTurretDrop;
    public GameObject barricadeDrop;
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;
    public GameObject Gun4;
    public GameObject Gun5;


    public int gameScore = 0;
    public int wave = 0;
    public int difficultyMultiplier = 1;
    public int zombiesAlive = 0;
    public int bEPieces = 0;
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
        Win,
        Lose
    }

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_GameState = GameState.Menu;
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

    public void PlayGame()
    {
        StartCoroutine("LoadLevel");
    }

    public void EndGame()
    {
        StartCoroutine("LoadLevel");
    }

    public IEnumerator LoadLevel()
    {
        if (m_GameState == GameState.Menu)
        {
            yield return SceneManager.LoadSceneAsync("Level");
            playButtonPressed = true;
        }
        
        if (m_GameState == GameState.Lose)
        {
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
        }
    }

    void Init()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));                            // remove old player object
        Destroy(GameObject.FindGameObjectWithTag("CameraRig"));
        Destroy(GameObject.FindGameObjectWithTag("Radio"));                             // remove old radio object
        Destroy(GameObject.FindGameObjectWithTag("SupplyDrop"));
        Destroy(GameObject.FindGameObjectWithTag("BEDrop"));

        playerSpawn = GameObject.Find("PlayerSpawn");
        radioSpawn = GameObject.Find("RadioSpawn");
        supplyDropSpawn = GameObject.Find("SupplyDropSpawn");
        zombieSpawner = GameObject.Find("SpawnManager");
        spawns = zombieSpawner.GetComponent<ZombieSpawner>();
        supplyDropSpawn = GameObject.Find("SupplyDropSpawn");
        bEDropSpawn = GameObject.Find("BEDropSpawn");
        weaponDropSpawn = GameObject.Find("WeaponDropSpawn");

    // destroy all spawnable game objects that may remain from previous game
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");             // find all of the zombies that are left over from the last game and store them in an array
        GameObject[] Traps = GameObject.FindGameObjectsWithTag("Traps");
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        GameObject[] Turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] Dropped = GameObject.FindGameObjectsWithTag("Dropped");

        foreach (GameObject z in zombies)
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

        foreach (GameObject tt in Turrets)
        {
            Destroy(tt);
        }

        foreach (GameObject d in Dropped)
        {
            Destroy(d);
        }

        // spawn new player object
        currentPlayer = Instantiate(playerObject, playerSpawn.transform.position, playerSpawn.transform.rotation);
        Instantiate(cameraRigObject);
        Instantiate(radioObject, radioSpawn.transform);

        playerHealth = currentPlayer.GetComponent<Health>();

        // reset variables (score, wave number, resources, etc.)
        wave = 0;
        gameScore = 0;
        zombiesAlive = 0;
        bEPieces = 0;
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

    void SpawnBEDrop()
    {
        if (currentBEDrop == null)
        {
            currentBEDrop = Instantiate(bEDropObject, bEDropSpawn.transform.position, bEDropSpawn.transform.rotation);
            hasBEDropSpawned = true;
        }
    }

    public void SelectedWeaponDrop()
    {
        if (wave == 1)
        {
              weaponDropObject = Gun1;
        }
        if (wave == 2)
        {
            weaponDropObject = Gun2;
        }
        if (wave == 3)
        {
            weaponDropObject = Gun3;
        }
        if (wave == 4)
        {
            weaponDropObject = Gun4;
        }
        if (wave == 5)
        {
            weaponDropObject = Gun5;
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
                
                spawnTimer += Time.deltaTime;
                actionPhaseActive = true;

                if (playerHealth.deathIsFinished == true || isRadioDead)
                {
                    m_GameState = GameState.Lose;
                }

                // change hud
                if ( spawnTimer >= spawnDeactivate)
                {
                    spawns.mayspawn = false;
                    canZombiesSpawn = false;
                }

                if ( spawnTimer >= bEDropTimeEnd && !hasBEDropSpawned)
                {
                    SpawnBEDrop();
                }

                if (bEPieces == 5)
                {
                    m_GameState = GameState.Win;
                }

                // All zombies dead change to build state
                if (!canZombiesSpawn && spawnTimer >= 3 && zombiesAlive == 0 && !bEPiecePickedUp)
                {
                    m_GameState = GameState.Build;
                }

                break;
            case GameState.Build:
                m_GameTime += Time.deltaTime;
                actionPhaseActive = false;
                GameObject[] Dropped = GameObject.FindGameObjectsWithTag("Dropped");

                // change hud to building hud

                if (m_GameTime >= 30)
                {
                    m_GameState = GameState.Action;
                    m_GameTime = 0f;
                    spawns.mayspawn = true;
                    canZombiesSpawn = true;
                    spawnTimer = 0f;
                    hasSupplyDropSpawned = false;
                    hasBEDropSpawned = false;
                    foreach (GameObject d in Dropped)
                    {
                        Destroy(d);
                    }
                    canGunBeSpawned = true;
                    wave += 1;
                    difficultyMultiplier = wave + 1;
                }

                // Make the supply drop spawn (set Spawn bool to true) (can only spawn when false)
                if (!hasSupplyDropSpawned)
                {
                    SpawnSupplyDrop();
                }

                if (canGunBeSpawned)
                {
                    SelectedWeaponDrop();
                    currentWeaponDrop = Instantiate(weaponDropObject, weaponDropSpawn.transform.position, weaponDropSpawn.transform.rotation);
                    canGunBeSpawned = false;
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
            case GameState.Lose:
                Debug.Log("You lose");
                EndGame();


                break;
            case GameState.Win:



                break;
        }
    }

    private void OnGUI()
    {
        if (deBugMod)
        {
            GUI.Label(new Rect(10, 10, 200, 20), "GameState =" + m_GameState);
        }
    }
}
