using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSprite : MonoBehaviour
{

    SpriteRenderer playerSprite;
    public SpriteRenderer headSprite;
    public Movement movement;
    public Animator bodyAnimator;
    public Animator headAnimator;
    float directX;
    float directZ;
    int headUp;
    int headSide;
    public float turnPoint = 5;
    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        directX = movement.directionX;
        directZ = movement.directionZ;
    }

    private void Update()
    {
        // make sprite change happen every frame
        BodySpriteChange();
        HeadSpriteChange();
    }

    void BodySpriteChange()
    {
        // use movement direction from movement script
        directX = movement.directionX;
        directZ = movement.directionZ;
        //if player is moving
        if (directX != 0 || directZ != 0)
        {
            if (directX == 1)
            {
                playerSprite.flipX = true;
                if (directZ == 0)
                    bodyAnimator.SetInteger("State", 2);
            }
            else if (directX == -1)
            {
                playerSprite.flipX = false;
                bodyAnimator.SetInteger("State", 2);
            }
            if (directZ < 0)
            {
                if (directX != 0)
                    bodyAnimator.SetInteger("State", 2);
                else
                    bodyAnimator.SetInteger("State", 1);
            } else if (directZ > 0)
            {
                if (directX != 0)
                    bodyAnimator.SetInteger("State", 3); // state 3 is siderun
                else
                    bodyAnimator.SetInteger("State", 4);
            }

        }
        //if player isn't moving stop running
        else
            bodyAnimator.SetInteger("State", 0);

    }

    void HeadSpriteChange()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (direction.normalized.y > 0)
        {
            headUp = 1;

        }
        else headUp = -1;
        if (direction.normalized.x <= -.5f)
        {
            headSide = -1; // if looking left
        } else if (direction.normalized.x > -0.5f && direction.normalized.x < 0.5f)
        {
            headSide = 0; //if looking straight
        } else if (direction.normalized.x >= .5f) headSide = 1; // if looking right

        if (headUp == 1)
        {
            if (headSide == 1)
            {
                //set int in headanimator so head points up and to the side
                headAnimator.SetInteger("Direction", 3);
                headSprite.flipX = false;
            } else if (headSide == 0)
            {
                //set int in headanimator so head points up
                headAnimator.SetInteger("Direction", 2);
            } else
            {
                //set int in headanimator so head points up and to the side
                headAnimator.SetInteger("Direction", 3);
                headSprite.flipX = true;
            }
            
        } else if (headUp == -1)
        {
            if (headSide == 1)
            {
                //set int in headanimator so head points down and to the side
                headAnimator.SetInteger("Direction", 1);
                headSprite.flipX = true;
            }
            else if (headSide == 0)
            {
                //set int in headanimator so head points down
                headAnimator.SetInteger("Direction", 0);
            }
            else
            {
                //set int in headanimator so head points down and to the side
                headAnimator.SetInteger("Direction", 1);
                headSprite.flipX = false;
            }
        }
        Debug.Log("Head up/down direction is " + headUp + ", and Head left/right direction is " + headSide);
    }
}
