using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;

public class Board
{

    public bool whitesTurn { get; private set; }

    public Position position {get; private set;}

    public Board()
    {
        whitesTurn = true;
        position = new Position();
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
                t[x, y] = new Tile(x, y, (x % 2) == (y % 2));
            }
        }
    }
}

public class Tile
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Piece piece;
    public GameObject theGO { get; private set; }

    bool isLight;
    public Tile(int x, int y, bool isLight)
    {
        this.x = x;
        this.y = y;
        this.isLight = isLight;
        Vector3 pos = new Vector3(Utilities.firstTile.x + (x * Utilities.tileWidth), Utilities.firstTile.y + (y * Utilities.tileWidth), 0f);
        if (isLight)
            theGO = UnityEngine.Object.Instantiate(Utilities.lightTile, pos, quaternion.identity);
        else
            theGO = UnityEngine.Object.Instantiate(Utilities.darkTile, pos, quaternion.identity);
        piece = Utilities.getPiece(x, y, this);
    }
}
