using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wrench : MonoBehaviour
{
    public float forceMagnitude = 10, damage = 30, initialCooldown = 3, repairAmount = 5;
    float cooldown = 0;
    bool attacking, repair;
    int rotation = 1, count = 0;
    public int swingSpeed = 6;
    public string[] repairableTags;
    public GameObject wrenchSprites, wind, windSpawnPoint;
    public GameObject hitFX; // stolen from james >:D
    public GameObject sparks;
    public bool tutorial = false;
    
    public AudioClip smackSFX, repairSFX, repairMissSFX;
    AudioSource wrenchSFX;

    GameObject ammoText, weaponText;

    List<GameObject> damaged = new List<GameObject>();//make sure nothing is hurt twice//repaired twice in one swipe.
    private void Start()
    {
        wrenchSFX = GetComponent<AudioSource>();
        ammoText = GameObject.Find("Bullet Count"); //lots of work wasted since i didnt know gameobject.find finds children of obj too.
        weaponText = GameObject.Find("Weapon Name");
        if (ammoText == null)
            return;
        weaponText.GetComponent<Text>().text = "Wrench";
        ammoText.GetComponent<Text>().text = "1";
        
    }
    private void OnEnable()
    {
        if (ammoText == null)
        {
            ammoText = GameObject.Find("Bullet Count");
            weaponText = GameObject.Find("Weapon Name");
            if (ammoText == null)
            {
                return;
            }
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().reloadGUIObject.SetActive(false);
        }
            
        weaponText.GetComponent<Text>().text = "Wrench";
        if (cooldown <= 0)
            ammoText.GetComponent<Text>().text = "1";
        else
            ammoText.GetComponent<Text>().text = "0";
    }
    private void Update()
    {
        if (Input.GetButton("Repair"))
        {
            repair = true;
        } else repair = false;
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
           /* if (ammoText != null)
            {
                ammoText.GetComponent<Text>().color = new Color(1, (initialCooldown - cooldown) / initialCooldown, (initialCooldown - cooldown) / initialCooldown);
            }*/
        } else
        {
            if (ammoText == null)
            {
                ammoText = GameObject.Find("Bullet Count");
                weaponText = GameObject.Find("Weapon Name");
                if (ammoText == null)
                {
                    return;
                }
            }
            if (ammoText.GetComponent<Text>().text == "0")
            {
                ammoText.GetComponent<Text>().text = "1";
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
        {
            if (!attacking)
            {
                cooldown = initialCooldown;
                attacking = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;

                if (ammoText == null)
                {
                    ammoText = GameObject.Find("Bullet Count");
                   weaponText = GameObject.Find("Weapon Name");
                    if (ammoText != null)
                    {
                        ammoText.GetComponent<Text>().text = "0";
                    }
                } else
                {
                    ammoText.GetComponent<Text>().text = "0";
                }
            
                if (repair)
                {
                    wrenchSFX.clip = repairMissSFX;
                    Camera.main.GetComponent<ScreenShake>().CamShake(.8f, .3f);
                }
                else
                {
                    wrenchSFX.clip = smackSFX;
                    Camera.main.GetComponent<ScreenShake>().CamShake(1.5f, .3f);
                }
                    wrenchSFX.pitch = .6f;
                wrenchSFX.Play();
                
                Instantiate(wind, windSpawnPoint.transform.position, windSpawnPoint.transform.rotation);
                damaged.Clear();
            } 
        }
        
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            Swipe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie" && !repair)
        {
            for (int i = 0; i < damaged.Count; i++)
            {
                if (damaged[i] = other.gameObject) // wont effect repair items cuz under tag zombie if function
                {
                    return;
                }
            }
            damaged.Add(other.gameObject);
            Debug.Log("Zombie Hit");
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.position - new Vector3 (transform.position.x,0,transform.position.y)* forceMagnitude, ForceMode.Impulse);
            // other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            other.gameObject.GetComponent<ZombieHealth>().Hurt(damage, gameObject.transform.position, 1.4f);
            
            GameObject bloodFX = Instantiate(hitFX, other.transform.position, Quaternion.identity);
                bloodFX.GetComponent<ParticleSystem>().Play();
            
        }
        else if (repair)
        {
            for (int i = 0; i < damaged.Count; i++)
            {
                if (damaged[i] = other.gameObject) // wont effect repair items cuz under tag zombie if function
                {
                    return;
                }
            }
            damaged.Add(other.gameObject);
            Repair(other.gameObject);
        }
    }
    

    void Swipe()
    {
        
        if (rotation == 1 && count < swingSpeed)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, -60 / (swingSpeed / 2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == 1 && attacking)
        {
            rotation = -1;
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }

        if (rotation == -1 && count < swingSpeed && attacking)
        {
            count++;
            wrenchSprites.transform.localRotation = Quaternion.Euler(0, 60 / (swingSpeed/2) * (swingSpeed / 2 - count), 0);
        }
        else if (count == swingSpeed && rotation == -1)
        {
            rotation = 1;
            attacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            count = 0;
        }


    }

    void Repair(GameObject repairable)
    {
       
        for (int i = 0; i < repairableTags.Length; i++)
        {
            if (repairable.tag == repairableTags[i])
            {
                Instantiate(sparks, repairable.transform.position + new Vector3 (0,.5f,0), Quaternion.identity);
                wrenchSFX.clip = repairSFX;
                wrenchSFX.pitch = .8f;
                wrenchSFX.Play();
                if (i == 0)
                {
                    
                    if (repairable.GetComponent<ExplosiveMine>() != null)
                    {
                        repairable.GetComponent<ExplosiveMine>().isDestroyed = false;
                        repairable.GetComponentInChildren<SpriteRenderer>().sprite = repairable.GetComponent<ExplosiveMine>()._sprite;
                    }
                    else
                    {
                        if (repairable.GetComponent<BearTrap>().uses < 3)
                        {
                                repairable.GetComponent<BearTrap>().uses += 1;
                        }
                            
                        repairable.GetComponent<BearTrap>().isDestroyed = false;
                        repairable.GetComponent<BearTrap>().animator.SetBool("Destroyed", false);
                    }
                }
                else if (i == 1)
                {
                    if (repairable.GetComponent<Barricade>().barricadeHealth < 60)
                    {
                        repairable.GetComponent<Barricade>().barricadeHealth = Mathf.Clamp(repairable.GetComponent<Barricade>().barricadeHealth + repairAmount * 2, 0, 60);
                        
                    }
                }
                else if (i == 2)
                {
                    if (tutorial)
                    {
                        if (repairable.GetComponent<TutorialTurret>().turretHealth < 100)
                        {

                            if (repairable.GetComponent<TutorialTurret>().turretHealth <= 0)
                            {
                                repairable.GetComponent<TutorialTurret>().turretHealth = repairAmount;
                                //repairable.GetComponent<Turret>().ActivateTurret();
                            }
                            else
                                repairable.GetComponent<TutorialTurret>().turretHealth = Mathf.Clamp(repairable.GetComponent<TutorialTurret>().turretHealth + repairAmount * 2, 0, 50);
                        }
                    }
                     if (repairable.GetComponent<Turret>().turretHealth < 100)
                    {
                        
                        if (repairable.GetComponent<Turret>().turretHealth <= 0)
                        {
                            repairable.GetComponent<Turret>().turretHealth = repairAmount;
                            //repairable.GetComponent<Turret>().ActivateTurret();
                        } else
                        repairable.GetComponent<Turret>().turretHealth = Mathf.Clamp(repairable.GetComponent<Turret>().turretHealth + repairAmount * 2, 0, 50);
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        /*if (ammoText == null)
        {
           // ammoText = GameObject.Find("Bullet Count");
          //weaponText = GameObject.Find("Weapon Name");
            if (ammoText == null)
            {
                return;
            }
        }
        ammoText.GetComponent<Text>().color = ammoTxtInit;*/
    }
}
