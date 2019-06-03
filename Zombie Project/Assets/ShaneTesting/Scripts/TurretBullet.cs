using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Transform target;
    public float bulletSpeed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;  
       float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)  //if length of our ray (dir) is less than the distance to target then we hav hit something so run the HitTarget() Function
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);  // move the bullet 

       

    }

   void HitTarget()
    {
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
}
