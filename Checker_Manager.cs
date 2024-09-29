using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.PlasticSCM.Editor.WebApi;

public class Checker_Manager : MonoBehaviour
{

    public static Checker_Manager instance { get; private set; }
    private Checker selectedChecker;

    private void Awake()
    {
        instance = this;
    }


    public void setSelectedChecker(Checker checker)
    {
        if (checker.player == "Top" && Game_Manager.currentPlayer == "Top_Player")
        {
            selectedChecker = checker;
            Debug.Log("Checker Row/Col: " + selectedChecker.row + "/" + selectedChecker.col);
        }
        if (checker.player == "Bottom" && Game_Manager.currentPlayer == "Bottom_Player")
        {
            selectedChecker = checker;
            Debug.Log("Checker Row/Col: " + selectedChecker.row + "/" + selectedChecker.col);
        }
    }



    public void moveSelectedChecker(Tile tileToMoveTo)
    {
        if (selectedChecker != null)
        {
            string moveResult = findMoveResult(tileToMoveTo, selectedChecker);
            Debug.Log(moveResult);

            // End early on invalid move
            if (moveResult == "MOVE_NOT_VALID_CHECKER_ON_TILE" || moveResult == "MOVE_NOT_VALID_PASSED_CHECKS")
            {
                return;
            }

            // Valid moves
            if (moveResult == "MOVE_VALID_KILLED_FOREWARD") {
            }
            if (moveResult == "MOVE_VALID_FOREWARD")
            {
                Game_Manager.NextTurn();
            }
            selectedChecker.transform.position = new Vector2(tileToMoveTo.transform.position.x, tileToMoveTo.transform.position.y);
            selectedChecker.row = tileToMoveTo.row;
            selectedChecker.col = tileToMoveTo.col;
            if (isNewKing())
            {
                Debug.Log("isNewKing = true");
                selectedChecker.isKing = true;
                selectedChecker.changeToKingSprite();

            }
            selectedChecker = null;
        } else
        {
            Debug.Log("Selected checker is null");
        }
    }

    private string findMoveResult(Tile tileToCheck, Checker movingChecker)
    {

        int rowChange = Math.Abs(tileToCheck.row - movingChecker.row);
        int columnChange = Math.Abs(tileToCheck.col - movingChecker.col);

        if (movingChecker.isKing)
        {
        // moving any direction once (As a king)
        if (rowChange == 1 && (columnChange == 0 || columnChange == 1))
        {
            return "MOVE_VALID_KING";
        }
        if (columnChange == 1 && (rowChange == 0 || rowChange == 1))
        {
            return "MOVE_VALID_KING";
        }
        // jumping over openent (As a King)
        return false;
        // Update Checkers
        // Otherwise False
        */
        }
        else
        {

            if (isCheckerOnTile(tileToCheck.row, tileToCheck.col))
            {
                return "MOVE_NOT_VALID_CHECKER_ON_TILE";
            }

            // Can either be "Top_Player" or "Bottom_Player"
            string currentPlayer = Game_Manager.currentPlayer;
            Debug.Log("currentPlayer string: " + currentPlayer + " | " + "Tile Row/Col: " + tileToCheck.row + "/" + tileToCheck.col + " | " + "Checker Row/Col: " + selectedChecker.row + "/" + selectedChecker.col);
            // True when ..
            // moving forward (Relative to current player) once
            if (currentPlayer == "Top_Player" && tileToCheck.row == selectedChecker.row + 1 && columnChange == 0)
            {
                return "MOVE_VALID_FOREWARD";
            }
            if (currentPlayer == "Bottom_Player" && tileToCheck.row == selectedChecker.row - 1 && columnChange == 0)
            {
                return "MOVE_VALID_FOREWARD";
            }
            // jumping diagonally forward (Relative to current player) over oponent
            if (currentPlayer == "Top_Player" && tileToCheck.row == selectedChecker.row + 2)
            {
                // jumping down left
                if (tileToCheck.col == selectedChecker.col - 2)
                {
                    Checker enemyChecker = getCheckerByRowCol(selectedChecker.row + 1, selectedChecker.col - 1);
                    if (isCheckerEnemy(enemyChecker))
                    {
                        Checker_Graveyard.AddCheckerToGraveyard(enemyChecker);
                        return "MOVE_VALID_KILLED_OPONENT";
                    }
                }
                // jumping down right
                if (tileToCheck.col == selectedChecker.col + 2)
                {
                    Checker enemyChecker = getCheckerByRowCol(selectedChecker.row + 1, selectedChecker.col + 1);
                    if (isCheckerEnemy(enemyChecker))
                    {
                        Checker_Graveyard.AddCheckerToGraveyard(enemyChecker);
                        return "MOVE_VALID_KILLED_OPONENT";
                    }
                }
            }
            if (currentPlayer == "Bottom_Player" && tileToCheck.row == selectedChecker.row - 2)
            {
                // jumping up left
                if (tileToCheck.col == selectedChecker.col - 2)
                {
                    Checker enemyChecker = getCheckerByRowCol(selectedChecker.row - 1, selectedChecker.col - 1);
                    if (isCheckerEnemy(enemyChecker))
                    {
                        Checker_Graveyard.AddCheckerToGraveyard(enemyChecker);
                        return "MOVE_VALID_KILLED_OPONENT";
                    }
                }
                // jumping up right
                if (tileToCheck.col == selectedChecker.col + 2)
                {
                    Checker enemyChecker = getCheckerByRowCol(selectedChecker.row - 1, selectedChecker.col + 1);
                    if (isCheckerEnemy(enemyChecker))
                    {
                        Checker_Graveyard.AddCheckerToGraveyard(enemyChecker);
                        return "MOVE_VALID_KILLED_OPONENT";
                    }
                }
            }
        }
        

        return "MOVE_NOT_VALID_PASSED_CHECKS";


    }

    bool isNewKing()
    {
        if (selectedChecker.player == "Top" && selectedChecker.row == 8)
        {
            return true;
        }
        if (selectedChecker.player == "Bottom" && selectedChecker.row == 1)
        {
            return true;
        }
        return false;
    }

    private bool isCheckerOnTile(int row, int col)
    {
        Checker[] currentCheckers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in currentCheckers)
        {
            if (checker.row == row && checker.col == col)
            {
                return true;
            }
        }
        return false;
    }

    private bool isCheckerEnemy(Checker checker)
    {
                if (Game_Manager.currentPlayer == "Top_Player" && checker.CompareTag("Bottom_Player"))
                {
                    return true;
                }
                if (Game_Manager.currentPlayer == "Bottom_Player" && checker.CompareTag("Top_Player"))
                {
                    return true;
                }
        return false;
    }

    private Checker getCheckerByRowCol(int row, int col)
    {
        Checker[] checkers = GameObject.FindObjectsOfType<Checker>();
        foreach (Checker checker in checkers)
        {
            if (checker.row == row && checker.col == col)
            {
                return checker;
            }
        }
        Debug.Log("checker not returned by row and col");
        return null;

    }
}


