using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform rig; // camerarig
    public float middle;
    public float radius = 4.5f;
    float distance; 
    Vector3 MousePos1, MousePos2, ScreenMouse, MouseOffset;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("CamFollowPoint").transform;
    }
    public void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("CamFollowPoint").transform;
        }

        MousePos1 = Input.mousePosition;
        // the below line assumes this script is attached to a camera rigect
        ScreenMouse = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(MousePos1.x, MousePos1.y, rig.position.z - GetComponent<Camera>().transform.position.z));
        MouseOffset = ScreenMouse - player.position;
        MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));
        rig.position = new Vector3((MousePos2.x - player.position.x) / middle + player.position.x, (MousePos2.y - player.position.y) / middle + player.position.y,  player.position.z)  + new Vector3 (0,30,30);

        distance = Vector2.Distance(new Vector2(rig.position.x, rig.position.y), new Vector2(player.position.x, player.position.y));

        if (distance > radius)
        {
            var norm = MouseOffset.normalized;
            //rig.position.x = norm.x * radius + player.position.x;
            //rig.position.y = norm.y * radius + player.position.y;
            rig.position = new Vector3(norm.x * radius + player.position.x, norm.y * radius + player.position.y, rig.position.z);
        }
    }
}
