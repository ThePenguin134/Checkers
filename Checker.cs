using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public int row;
    public int col;
    public string player;
    public bool isKing = false;
    public SpriteRenderer spriteRenderer;
    public Sprite redKingSprite;
    public Sprite greyKingSprite;

    private void OnMouseDown()
    {
        Checker_Manager.instance.setSelectedChecker(this);
    }

    public void moveChecker(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    public void changeZPos(float newZPos)
    {
        Vector3 newPos = new(transform.position.x, transform.position.y, newZPos);
        transform.position = newPos;
    }

    public void changeToKingSprite()
    {
        if (player == "Top")
        {
            spriteRenderer.sprite = redKingSprite;
        }
        if (player == "Bottom")
        {
            spriteRenderer.sprite = greyKingSprite;
        }
    }
}
