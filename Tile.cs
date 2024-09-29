using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float colorDelay;
    private float delayAfterClick;
    private Color originalColor;
    public Transform tileTransform;
    public int row;
    public int col;
    public bool isCheckerPresent;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        delayAfterClick = delayAfterClick + Time.deltaTime;
        if (delayAfterClick > colorDelay)
        {
            spriteRenderer.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = Color.green;
        delayAfterClick = 0;
        Checker_Manager.instance.moveSelectedChecker(this);
    }
}