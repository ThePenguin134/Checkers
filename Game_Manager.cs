using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }
    public static string currentPlayer;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = "Top_Player";
    }

    public static void NextTurn()
    {
        Debug.Log("Next turn started");
        if (currentPlayer == "Top_Player")
        {
            currentPlayer = "Bottom_Player";
        } else if (currentPlayer == "Bottom_Player")
        {
            currentPlayer = "Top_Player";
        }
        Debug.Log("Current Player:" + currentPlayer);
    }
}
