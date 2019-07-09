using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public bool gameEnd = false;
    
    public float m_GameTime = 0f;
    public float GameTime { get { return m_GameTime; } }
    public float timeUntilEndOfWave = 30f;
    public float spawnTimer = 0f;
    public float timeUntilBEDrop = 0f;
    public float spawnRepeatRate = 3.75f;

    public Health playerHealth;
    public ZombieSpawner spawns;

    public GameObject supplyDropSpawn;
    public GameObject supplyDropSpawn2;
    public GameObject supplyDropSpawn3;
    public GameObject supplyDropSpawn4;
    public GameObject bEDropSpawn;
    public GameObject bEDropSpawn2;
    public GameObject bEDropSpawn3;
    public GameObject bEDropSpawn4;
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
    public GameObject currentSupplyDropSpawn;
    public GameObject currentBEDrop;
    public GameObject currentBEDropSpawn;
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
    public GameObject reloadGUIObject;

    public List<GameObject> corpses;
    public float maxNumberOfCorpses = 100f;
    public List<GameObject> shells;
    public float maxNumberOfShells = 200f;


    GameObject GameHUD;
    Text BEPiecesText;
    Text WaveNumberText;
    Text ZombiesRemainText;
    Text TimeRemainText;
    Text AmmoText;
    public Text BEUpdateText;

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
    public int currentAmmo = 0;

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
        CheckDespawns();
    }

    void CheckDespawns()
    {
        if (corpses.Count > maxNumberOfCorpses)         // if there are too many corpses
        {
            Destroy(corpses[0]);                        // destroy the oldest corpse
            corpses.RemoveAt(0);                        // remove reference from list
        }
        
        if (shells.Count > maxNumberOfShells)           // if there are too many shells
        {
            Destroy(shells[0]);                         // destroy the oldest shell
            shells.RemoveAt(0);                         // remove reference from list
        }
    }

    public void PlayGame()
    {
        StartCoroutine("LoadLevel");
    }

    public void EndGame()
    {
        if (!gameEnd)
        {
        StartCoroutine("LoadLevel");
        }
    }

    public IEnumerator LoadLevel()
    {
        if (m_GameState == GameState.Menu)
        {
            yield return SceneManager.LoadSceneAsync("Level1");
            playButtonPressed = true;
        }
        
        if (m_GameState == GameState.Lose)
        {
            Debug.Log("You Lost!");
            yield return SceneManager.LoadSceneAsync("Lose");
            Destroy(gameObject);
        }

        if (m_GameState == GameState.Win)
        {
            yield return SceneManager.LoadSceneAsync("GameEnd");
        }
    }

    void Init()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));                            // remove old player object
        Destroy(GameObject.FindGameObjectWithTag("CameraRig"));
        Destroy(GameObject.FindGameObjectWithTag("Radio"));                             // remove old radio object
        Destroy(GameObject.FindGameObjectWithTag("SupplyDrop"));
        Destroy(GameObject.FindGameObjectWithTag("BEDrop"));

        GameObject[] dropped = GameObject.FindGameObjectsWithTag("Dropped");
        foreach (GameObject d in dropped)
        {
            Destroy(d);
        }


        playerSpawn = GameObject.Find("PlayerSpawn");
        radioSpawn = GameObject.Find("RadioSpawn");
        supplyDropSpawn = GameObject.Find("SupplyDropSpawn");
        supplyDropSpawn2 = GameObject.Find("SupplyDropSpawn2");
        supplyDropSpawn3 = GameObject.Find("SupplyDropSpawn3");
        supplyDropSpawn4 = GameObject.Find("SupplyDropSpawn4");
        zombieSpawner = GameObject.Find("SpawnManager");
        spawns = zombieSpawner.GetComponent<ZombieSpawner>();
        supplyDropSpawn = GameObject.Find("SupplyDropSpawn");
        bEDropSpawn = GameObject.Find("BEDropSpawn");
        bEDropSpawn2 = GameObject.Find("BEDropSpawn2");
        bEDropSpawn3 = GameObject.Find("BEDropSpawn3");
        bEDropSpawn4 = GameObject.Find("BEDropSpawn4");
        reloadGUIObject = GameObject.Find("ReloadReminder");
        reloadGUIObject.SetActive(false);

        GameHUD = GameObject.Find("GUI");
        BEPiecesText = GameObject.Find("Broadcast Equipment Pieces").GetComponent<Text>();
        WaveNumberText = GameObject.Find("Swarm Number").GetComponent<Text>();
        ZombiesRemainText = GameObject.Find("Zombies Remaining").GetComponent<Text>();
        TimeRemainText = GameObject.Find("Time Remaining").GetComponent<Text>();
        AmmoText = GameObject.Find("Ammo").GetComponent<Text>();
        BEUpdateText = GameObject.Find("BE Update").GetComponent<Text>();

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

        foreach (GameObject t in Traps)
        {
            Destroy(t);
        }

        foreach (GameObject b in barricades)
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
        wave = 1;
        gameScore = 0;
        zombiesAlive = 0;
        bEPieces = 0;
        mineStock = 0;
        bearTrapStock = 0;
        barricadeStock = 0;
        machineGunTurretStock = 0;
        javelinRocketTurretStock = 0;
        currentAmmo = 0;
        spawnTimer = timeUntilEndOfWave;
        timeUntilBEDrop = spawnTimer / 2;

        // change to the action state once everything is setup)
        m_GameState = GameState.Action;
    }

    void SpawnSupplyDrop()
    {

        if (currentSupplyDrop == null)
        {
            RandomSDSpawn();

            currentSupplyDrop = Instantiate(supplyDropObject, currentSupplyDropSpawn.transform.position, currentSupplyDropSpawn.transform.rotation);
            hasSupplyDropSpawned = true;
        }
    }

    void RandomSDSpawn()
    {
        int SDrandom = RandomValue(
        new RandomVaribles(1, 4, 1f));

        if (SDrandom == 1)
        {
            currentSupplyDropSpawn = supplyDropSpawn;
        }
        if (SDrandom == 2)
        {
            currentSupplyDropSpawn = supplyDropSpawn2;
        }
        if (SDrandom == 3)
        {
            currentSupplyDropSpawn = supplyDropSpawn3;
        }
        if (SDrandom == 4)
        {
            currentSupplyDropSpawn = supplyDropSpawn4;
        }
    }

    void SpawnBEDrop()
    {
        if (currentBEDrop == null)
        {
            RandomBEDSpawn();

            currentBEDrop = Instantiate(bEDropObject, currentBEDropSpawn.transform.position, currentBEDropSpawn.transform.rotation);
            hasBEDropSpawned = true;
            BEUpdateText.text = "Goal: Get the BE Drop. It has spawned somewhere around the map.";
        }
    }

    void RandomBEDSpawn()
    {
        int BEDrandom = RandomValue(
        new RandomVaribles(1, 4, 1f));

        if (BEDrandom == 1)
        {
            currentBEDropSpawn = bEDropSpawn;
        }
        if (BEDrandom == 2)
        {
            currentBEDropSpawn = bEDropSpawn2;
        }
        if (BEDrandom == 3)
        {
            currentBEDropSpawn = bEDropSpawn3;
        }
        if (BEDrandom == 4)
        {
            currentBEDropSpawn = bEDropSpawn4;
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

                // update HUD
                GameHUD.SetActive(true);
                BEPiecesText.text = "BE Pieces: " + bEPieces + "/5";
                WaveNumberText.text = "Wave: " + wave;
                ZombiesRemainText.text = "Zombies Remaining: " + zombiesAlive;
                BEUpdateText.text = "Goal: Kill the zombies & survive.";

                double spawnTimerRounded;
                spawnTimerRounded = System.Math.Round(spawnTimer, 2);               // round the timer to 2 decimal places
                TimeRemainText.text = "Time Remaining: " + spawnTimerRounded;
                

                spawnTimer -= Time.deltaTime;           // count down spawn timer
                actionPhaseActive = true;

                if (playerHealth.deathIsFinished == true || isRadioDead)
                {
                    m_GameState = GameState.Lose;
                }

                if (spawnTimer <= 0)                    // spawn timer expired
                {
                    spawns.mayspawn = false;
                    canZombiesSpawn = false;
                    spawnTimer = 0;                      // keep timer at 0
                }

                if ( spawnTimer <= timeUntilBEDrop && !hasBEDropSpawned && !bEPiecePickedUp)            // drop broadcast equipment
                {
                    SpawnBEDrop();
                }

                if (bEPieces == 5)
                {
                    m_GameState = GameState.Win;
                }

                // All zombies dead change to build state
                if (!canZombiesSpawn && spawnTimer <= 0 && zombiesAlive == 0)
                {
                    m_GameState = GameState.Build;
                }

                break;
            case GameState.Build:

                GameHUD.SetActive(false);

                m_GameTime += Time.deltaTime;
                actionPhaseActive = false;
                GameObject[] Dropped = GameObject.FindGameObjectsWithTag("Dropped");


                // Make the supply drop spawn (set Spawn bool to true) (can only spawn when false)
                if (!hasSupplyDropSpawned)
                {
                    SpawnSupplyDrop();
                }

                if (canGunBeSpawned)
                {
                    SelectedWeaponDrop();
                    currentWeaponDrop = Instantiate(weaponDropObject, currentSupplyDropSpawn.transform.position, currentSupplyDropSpawn.transform.rotation);
                    canGunBeSpawned = false;
                }

                if (m_GameTime >= 45)
                {
                    m_GameState = GameState.Action;
                    m_GameTime = 0f;
                    timeUntilEndOfWave += difficultyMultiplier * 5;
                    spawnRepeatRate -= difficultyMultiplier * 0.3f; 
                    if (spawnRepeatRate <= 0.5f)
                    {
                        spawnRepeatRate = 0.5f;
                    }
                    spawns.mayspawn = true;
                    canZombiesSpawn = true;
                    spawnTimer = 0f;
                    hasBEDropSpawned = false;
                    foreach (GameObject d in Dropped)
                    {
                        Destroy(d);
                    }
                    canGunBeSpawned = true;
                    wave += 1;
                    difficultyMultiplier = wave + 1;
                    spawnTimer = timeUntilEndOfWave;
                    timeUntilBEDrop = spawnTimer / 2;
                    bEPiecePickedUp = false;
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
            case GameState.Lose:
                Debug.Log("You lose");
                EndGame();


                break;
            case GameState.Win:
                Debug.Log("You wine");
                EndGame();
                gameEnd = true;
                break;
        }
    }

    struct RandomVaribles
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomVaribles(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    int RandomValue(params RandomVaribles[] varibles)
    {
        float randomness = Random.value;
        float currentProbability = 0;
        foreach (var varible in varibles)
        {
            currentProbability += varible.probability;
            if (randomness <= currentProbability)
                return varible.GetValue();
        }

        return 1;
    }

    private void OnGUI()
    {
        if (deBugMod)
        {
            GUI.Label(new Rect(10, 10, 200, 20), "GameState =" + m_GameState);
        }
    }
}
