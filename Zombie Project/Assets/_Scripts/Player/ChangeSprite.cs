using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    float vertMovementInputValue;
    float horMovementInputValue;
    SpriteRenderer playerSprite;
    public Sprite[] playerSprites;
    public SpriteRenderer playerGun;
    private void Awake()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        vertMovementInputValue = Input.GetAxis("Vertical");
        horMovementInputValue = Input.GetAxis("Horizontal");
        SpriteChange();
    }
    void SpriteChange()
    {
        if (vertMovementInputValue != 0 || horMovementInputValue != 0)
        {
            if ((vertMovementInputValue < 0 && horMovementInputValue < 0) || (vertMovementInputValue == 0 && horMovementInputValue < 0))
            {
                playerSprite.sprite = playerSprites[2];
                playerGun.sortingOrder = 1;
            }
            else if (vertMovementInputValue < 0 && horMovementInputValue == 0)
            {
                playerSprite.sprite = playerSprites[0];
                playerGun.sortingOrder = 1;
            }
            else if ((vertMovementInputValue < 0 && horMovementInputValue > 0) || (vertMovementInputValue == 0 && horMovementInputValue > 0))
            {
                playerSprite.sprite = playerSprites[1];
                playerGun.sortingOrder = 1;
            }
            else if (vertMovementInputValue > 0 && horMovementInputValue == 0)
            {
                playerSprite.sprite = playerSprites[3]; playerGun.sortingOrder = -1;
            }
            else if (vertMovementInputValue > 0 && horMovementInputValue > 0)
            {
                playerSprite.sprite = playerSprites[4]; playerGun.sortingOrder = -1;
            }
            else if (vertMovementInputValue > 0 && horMovementInputValue < 0)
            {
                playerSprite.sprite = playerSprites[5]; playerGun.sortingOrder = -1;
            }
        }
        else
        {
            playerSprite.sprite = playerSprites[0];
            playerGun.sortingOrder = 1;
        }
    }
}
