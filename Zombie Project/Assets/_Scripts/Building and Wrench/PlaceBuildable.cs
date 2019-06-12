using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildable : MonoBehaviour
{
    public int quantity = 0;
    int colliderAmount = 0;

    LayerMask ground;

    public bool isSprite;
    bool placeable;

    public float radius = 5;

    public Material objectMaterial; 
    GameObject player;
    public GameObject placedObject;

    SpriteRenderer spriteComponent;


    float distance;
    Vector3 yPosAdd;
    void Start()
    {
        ground = LayerMask.GetMask("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
        if (isSprite)
        {
            spriteComponent = GetComponent<SpriteRenderer>();
            if (spriteComponent == null)
            {
                spriteComponent = GetComponentInChildren<SpriteRenderer>();
            }
        }
        
    }

    
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            if (isSprite)
            {
                yPosAdd = new Vector3(0, .005f + hit.point.y, .005f); // elevates it above floor so it doesn't clip
            }
            else
                yPosAdd = new Vector3(0, hit.point.y, 0);


            distance = Vector2.Distance(new Vector2(hit.point.x, hit.point.z), new Vector2(player.transform.position.x, player.transform.position.z));
            if (distance > radius)
            {
                Vector3 norm = (hit.point - player.transform.position).normalized;
                transform.position = new Vector3(norm.x * radius + player.transform.position.x, 0, norm.z * radius + player.transform.position.z) + yPosAdd;
            }
            else transform.position = new Vector3(hit.point.x, 0, hit.point.z) + yPosAdd;

            
        }

        
        //scroll rotates object
        if (Input.mouseScrollDelta.y != 0)
        {
            if (!isSprite)
            gameObject.transform.Rotate(0, Input.mouseScrollDelta.y * 10, 0);
            //if (isSprite && spriteComponent.gameObject != gameObject)
            //{
            //    spriteComponent.gameObject.transform.rotation = Quaternion.identity;
            //    spriteComponent.gameObject.transform.Rotate(0, 45, 0, Space.World);
            //}

        }


        if (quantity > 0)
        {
            if (placeable && Input.GetKeyUp(KeyCode.Mouse0))
            {
                PlaceObject();
            }
        } else gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "PlaceableGround")
        {
            Debug.Log("Object '" + other.name + "' entered placeable collider.");
            placeable = false;
            colliderAmount++;
            if (isSprite)
            {
                spriteComponent.color = new Color(1, 0, 0, .5f);
            } else
            objectMaterial.color = new Color(1, 0, 0, .5f);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "PlaceableGround")
        {
            colliderAmount--;
        }
        if(colliderAmount <= 0)
        {
            placeable = true;
            if (isSprite)
            {
                spriteComponent.color = new Color(0, 1, 0, .5f);
            } else
            objectMaterial.color = new Color(0, 1, 0, .5f);
        }
    }

    public void Reset(int colliders, Color color)
    {
        colliderAmount = colliders;
        if (isSprite)
        {
            spriteComponent.color = color;
        }
        else
            objectMaterial.color = color;
    }

    void PlaceObject()
    {
        //instantiate buildable gameobject
        quantity--;
        Instantiate(placedObject, transform.position, transform.rotation);
        if (quantity <= 0)
        {
            if (isSprite)
            {
                spriteComponent.color = new Color(0, 1, 0, .5f);
            }
            else
                objectMaterial.color = new Color(0, 1, 0, .5f);
            gameObject.SetActive(false);
        }
        
    }
}
