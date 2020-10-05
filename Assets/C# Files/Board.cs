using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.UI;
using UnityEngine;

public class Board
{

    public bool whitesTurn { get; private set; }

    public Position position {get; private set;}

    public Board()
    {
        whitesTurn = true;
    }


}

public class Position
{
    public Tile[,] brd = new Tile[8, 8] ;

    public Position()
    {
        resetBrd(brd);
        
    }

    void resetBrd(Tile[,] t) {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                t[x, y] = new Tile(x, y, x == y);
            }
        }
    }
}

public class Tile
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Piece piece;

    bool isLight;
    public Tile(int x, int y, bool isLight)
    {
        this.x = x;
        this.y = y;
        this.isLight = isLight;
        this.piece = Utilities.getPiece(x, y, this);
    }
}
