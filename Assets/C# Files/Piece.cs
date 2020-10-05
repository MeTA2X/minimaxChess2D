using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using TMPro;
using UnityEngine;

public class Piece
{
    public bool isWhite { get; private set; }
    public bool infinite { get; private set; }

    public int x;
    public int y;

    GameObject self;

    public Tile myTile;
    Board brd;

    public List<Tuple<int, int>> configuration;

    public Piece(int x, int y, bool isWhite, Tile t, bool infinite=false)
    {
        this.infinite = infinite;
        this.x = x;
        this.y = y;
        this.isWhite = isWhite;
    }

    public virtual List<Tuple<int,int>> getPossibleMoves()
    {
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
        if (infinite) {
            foreach(Tuple<int,int> cfg in configuration)
            {
                int[] newlocation = new int[2];
                newlocation[0] = cfg.Item1+x;
                newlocation[1] = cfg.Item2+y;
                for(int i = 0; i < 8; i++)
                {
                    //terminating cases
                    if (isValidMove(newlocation[0],newlocation[1])){
                        moves.Add(new Tuple<int,int>(newlocation[0], newlocation[1]));
                    }
                    else
                        break;
                    newlocation[0]++;
                    newlocation[1]++;
                }
            }
        } else {
            foreach(Tuple<int,int> cfg in configuration)
            {
                if (isValidMove(x + cfg.Item1, x + cfg.Item2))
                    moves.Add(new Tuple<int, int>(x + cfg.Item1, y + cfg.Item2));
            }
        }

        return null;
    }

    bool isValidMove(int x, int y)
    {
        var tlocation = Utilities.gm.board.position.brd[x, y];
        if (Utilities.isValidLocation(x, y) && (tlocation.piece == null || tlocation.piece.isWhite != isWhite))
            return true;
        return false;
    }

    public void changeTile(Tile newTile)
    {
        myTile = newTile;
        x = myTile.x;
        y = myTile.y;
        self.transform.position.x+=
    }
}

public class King : Piece
{
    public King(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {
        configuration.Add(new Tuple<int, int>(1,0));
    }
}

public class Queen : Piece
{
    public Queen(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {

    }
}

public class Rook : Piece
{
    public Rook(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {

    }
}

public class Knight : Piece
{
    public Knight(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {

    }
}

public class Bishop : Piece
{
    public Bishop(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {

    }
}

public class Pawn : Piece
{
    public Pawn(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {

    }
}