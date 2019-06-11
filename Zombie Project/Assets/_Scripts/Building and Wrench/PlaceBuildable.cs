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

    GameObject player;

    float distance, yPos;
    void Start()
    {
        ground = LayerMask.GetMask("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
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
            
            distance = Vector2.Distance(new Vector2(hit.point.x, hit.point.z), new Vector2(player.transform.position.x, player.transform.position.z));
            if (distance > radius)
            {
                Vector3 norm = (hit.point - player.transform.position).normalized;
                transform.position = new Vector3(norm.x * radius + player.transform.position.x, yPos, norm.z * radius + player.transform.position.z);
            }
            else transform.position = new Vector3(hit.point.x, yPos, hit.point.z);
        }

        
        //teleport to mouse-ground raycast

        //scroll should rotate object (if its a sprite could set sprite to parallel with ground then activate lookatscreen code)

        if (placeable && Input.GetKeyUp(KeyCode.Mouse0) && quantity > 0)
        {
            PlaceObject();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "PlaceableGround")
        {
            placeable = false;
            colliderAmount++;
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
        }
    }

    void PlaceObject()
    {
        //instantiate buildable gameobject
        quantity--;
    }
}
