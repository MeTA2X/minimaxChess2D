using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;
    
    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    public GameObject lightTile;
    public GameObject darkTile;
    void Start()
    {
        Utilities.whiteKing = whiteKing;
        Utilities.whiteQueen = whiteQueen;
        Utilities.whiteBishop = whiteBishop;
        Utilities.whiteKnight = whiteKnight;
        Utilities.whiteRook = whiteRook;
        Utilities.whitePawn = whitePawn;

        Utilities.blackKing = blackKing;
        Utilities.blackQueen = blackQueen;
        Utilities.blackBishop = blackBishop;
        Utilities.blackKnight = blackKnight;
        Utilities.blackRook = blackRook;
        Utilities.blackPawn = blackPawn;

        Utilities.lightTile = lightTile;
        Utilities.darkTile = darkTile;
        board = new Board();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
