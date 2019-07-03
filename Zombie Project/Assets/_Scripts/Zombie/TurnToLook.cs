using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToLook : MonoBehaviour
{
    Transform target;
    public void EnableTurn(Transform attackTarget, bool enable)
    {
        if (!enable)
        {
            target = null;
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        target = attackTarget;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
    private void OnDisable()
    {
        target = null;
    }
}
