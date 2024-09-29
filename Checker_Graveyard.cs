using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker_Graveyard : MonoBehaviour
{
    public static Checker_Graveyard Instance { get; private set; }
   
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

    private static int deadNum = 0;

    public static void AddCheckerToGraveyard(Checker deadChecker)
    {
        deadNum++;
        deadChecker.row = -1;
        deadChecker.col = -1;
        deadChecker.moveChecker(-6, -4 + (0.3f * deadNum));
        deadChecker.changeZPos(-deadNum);
    }
}
