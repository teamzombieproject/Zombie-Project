using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBuildable : MonoBehaviour
{
    public string placeableBuildableName;
    public int quantity = 0;
    bool attract = false;//, pickedUp;
    GameObject target, child;
    public ParticleSystem pickupFX;
    public float countdown = 2;
    public float min = 5, max = 10;
    bool collided;

    private void Start()
    {
        int random = Random.Range(0, 2) * 2 - 1;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(min, max) * random, 0, Random.Range(min, max) * random), ForceMode.Impulse);
        child = GetComponentInChildren<RecogniseCollision>().gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attract = true;
            target = other.gameObject;
        }
    }
    public void Pickup()
    {
        if (countdown > 0)
        {
            collided = true;
            return;
        }
        GameObject.FindGameObjectWithTag("BuildManager").GetComponent<BuildingInventory>().BuildablePickup(GameObject.FindGameObjectWithTag("BuildManager").transform.Find(placeableBuildableName).gameObject, quantity);
        Debug.Log("destroyed pickup");
        pickupFX.transform.SetParent(null);
        pickupFX.Play();
        pickupFX.GetComponent<AudioSource>().Play();
        Camera.main.GetComponent<ScreenShake>().CamShake(1f, .2f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (child.activeSelf == false && countdown <= 0)
        {
            child.SetActive(true);
        }
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            if (collided)
            {
                collided = false;
                child.SetActive(false);
            }
        }
        if (attract && countdown <= 0)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10 * Time.deltaTime);

    }

    private void LateUpdate()
    {
        
    }
}
