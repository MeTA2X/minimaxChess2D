using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Utilities
{
    public static GameObject blackKing;
    public static GameObject blackQueen;
    public static GameObject blackRook;
    public static GameObject blackBishop;
    public static GameObject blackKnight;
    public static GameObject blackPawn;

    public static GameObject whiteKing;
    public static GameObject whiteQueen;
    public static GameObject whiteRook;
    public static GameObject whiteBishop;
    public static GameObject whiteKnight;
    public static GameObject whitePawn;

    public static GameObject lightTile;
    public static GameObject darkTile;

    public static GameManager gm;

    public static float tileWidth = 1.0092f;

    public static bool isValidLocation(int x, int y) { return x <= 7 && x >= 0 && y <= 7 && y >= 0; }


    public static Piece getPiece(int x, int y, Tile tile)
    {
        if (y == 0 || y == 7)
        {
            if (x == 0 || x == 7)
                return new Rook(x, y, y == 0, tile);
            if (x == 1 || x == 6)
                return new Knight(x, y, y == 0, tile);
            if (x == 2 || x == 5)
                return new Bishop(x, y, y == 0, tile);
            if(x == 3)
            {
                if (y == 0)
                    return new Queen(x, y, true, tile);
                else
                    return new King(x, y, false, tile);
            }else if(x == 4)
            {
                if (y == 0)
                    return new King(x, y, true, tile);
                else
                    return new Queen(x, y, false, tile);
                
            }
        }
        else if (y == 1 || y == 6)
            return new Pawn(x, y, y == 1, tile);
        return null;
    }
}
