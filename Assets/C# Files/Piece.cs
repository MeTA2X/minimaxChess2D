using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEditorInternal;
using UnityEngine;

public abstract class Piece
{
    public bool isWhite { get; private set; }
    public bool infinite { get; private set; }
    public bool hasMoved { get; private set; }

    public int x;
    public int y;

    public GameObject self;

    public Tile myTile;
    Board brd;

    public List<Tuple<int, int>> configuration;

    public Piece(int x, int y, bool isWhite, Tile t, bool infinite=false)
    {
        configuration = new List<Tuple<int, int>>();
        this.infinite = infinite;
        this.x = x;
        this.y = y;
        this.isWhite = isWhite;
        myTile = t;
        createGameObject();
    }

    public abstract void createGameObject();

    public virtual List<Tuple<int,int>> getPossibleMoves()
    {
        bool terminating;
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
        if (infinite) {
            foreach(Tuple<int,int> cfg in configuration)
            {
                terminating = false;
                int[] newlocation = new int[2];
                newlocation[0] = cfg.Item1+x;
                newlocation[1] = cfg.Item2+y;
                for(int i = 0; i < 8; i++)
                {
                    //terminating cases
                    if (isValidMove(newlocation[0],newlocation[1], terminating)){
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
                terminating = false;
                if (Utilities.isValidLocation(x + cfg.Item1, x + cfg.Item2) && isValidMove(x + cfg.Item1, x + cfg.Item2, terminating))
                    moves.Add(new Tuple<int, int>(x + cfg.Item1, y + cfg.Item2));
            }
        }

        return null;
    }

    public bool isValidMove(int x, int y, bool terminating)
    {
        var tlocation = Utilities.gm.board.position.brd[x, y];
        if (Utilities.isValidLocation(x, y) && (tlocation.piece == null || tlocation.piece.isWhite != isWhite)) {
            if (tlocation.piece != null)
                terminating = true;
            return true;
        }
        return false;
    }

    public void changeTile(Tile newTile)
    {
        myTile = newTile;
        self.transform.position = new Vector3(self.transform.position.x + ((x - myTile.x) * Utilities.tileWidth), self.transform.position.y + ((y - myTile.y) * Utilities.tileWidth), self.transform.position.z);
        x = myTile.x;
        y = myTile.y;
    }
}

public class King : Piece
{
    public King(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {
        for(int configx = -1; configx < 2; configx++)
        {
            for(int configy = -1; configy < 2; configy++)
            {
                configuration.Add(new Tuple<int, int>(configx, configy));
            }
        }
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whiteKing, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackKing, myTile.theGO.transform.position, quaternion.identity);
    }
}

public class Queen : Piece
{
    public Queen(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t, true)
    {
        for (int configx = -1; configx < 2; configx++)
        {
            for (int configy = -1; configy < 2; configy++)
            {
                configuration.Add(new Tuple<int, int>(configx, configy));
            }
        }
        /*
        * configuration.Add(new Tuple<int, int>(-1, 1));
        * configuration.Add(new Tuple<int, int>(-1, 0));
        * configuration.Add(new Tuple<int, int>(-1, -1));
        * configuration.Add(new Tuple<int, int>(0, 1));
        * configuration.Add(new Tuple<int, int>(0, 0));
        * configuration.Add(new Tuple<int, int>(0, -1));
        * configuration.Add(new Tuple<int, int>(1, 1));
        * configuration.Add(new Tuple<int, int>(1, 0));
        * configuration.Add(new Tuple<int, int>(1, -1));
        */
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whiteQueen, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackQueen, myTile.theGO.transform.position, quaternion.identity);
    }
}

public class Rook : Piece
{
    public Rook(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t, true)
    {
        configuration.Add(new Tuple<int, int>(-1, 0));
        configuration.Add(new Tuple<int, int>(0, 1));
        configuration.Add(new Tuple<int, int>(0, -1));
        configuration.Add(new Tuple<int, int>(1, 0));
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whiteRook, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackRook, myTile.theGO.transform.position, quaternion.identity);
    }
}

public class Knight : Piece
{
    public Knight(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {
        configuration.Add(new Tuple<int, int>(-2, 1));
        configuration.Add(new Tuple<int, int>(-2, -1));
        configuration.Add(new Tuple<int, int>(-1, 2));
        configuration.Add(new Tuple<int, int>(-1, -2));
        configuration.Add(new Tuple<int, int>(1, 2));
        configuration.Add(new Tuple<int, int>(1, -2));
        configuration.Add(new Tuple<int, int>(2, 1));
        configuration.Add(new Tuple<int, int>(2, -1));
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whiteKnight, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackKnight, myTile.theGO.transform.position, quaternion.identity);
    }
}

public class Bishop : Piece
{
    public Bishop(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t, true)
    {
        configuration.Add(new Tuple<int, int>(1, 1));
        configuration.Add(new Tuple<int, int>(1, -1));
        configuration.Add(new Tuple<int, int>(-1, -1));
        configuration.Add(new Tuple<int, int>(-1, 1));
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whiteBishop, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackBishop, myTile.theGO.transform.position, quaternion.identity);
    }
}

public class Pawn : Piece
{
    public Pawn(int x, int y, bool isWhite, Tile t) : base(x, y, isWhite, t)
    {
        configuration.Add(new Tuple<int, int>(0, 1));
        configuration.Add(new Tuple<int, int>(0, 2));
        configuration.Add(new Tuple<int, int>(1, 1));
        configuration.Add(new Tuple<int, int>(-1, 1));
    }

    public override void createGameObject()
    {
        if (isWhite)
            self = UnityEngine.Object.Instantiate(Utilities.whitePawn, myTile.theGO.transform.position, quaternion.identity);
        else
            self = UnityEngine.Object.Instantiate(Utilities.blackPawn, myTile.theGO.transform.position, quaternion.identity);
    }

    public override List<Tuple<int, int>> getPossibleMoves()
    {
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
        Tuple<int, int> move1 = configuration[0];
        Tuple<int, int> move2 = configuration[1];
        Tuple<int, int> takeRight = configuration[2];
        Tuple<int, int> takeLeft = configuration[3];
        bool terminating = false;
        if (Utilities.isValidLocation(move1.Item1 + x, move1.Item2 + y) && isValidMove(move1.Item1 + x, move1.Item2 + y, terminating)){
            moves.Add(new Tuple<int, int>(move1.Item1 + x, move1.Item2 + y));
            if (terminating)
                goto SkipToHere;
            if (Utilities.isValidLocation(move2.Item1 + x, move1.Item2))
                moves.Add(new Tuple<int, int>(move2.Item1 + x, move1.Item2));
        }
    SkipToHere:
        if (Utilities.isValidLocation(takeRight.Item1 + x, takeRight.Item2 + y) && isValidMove(takeRight.Item1 + x, takeRight.Item2 + y, terminating)) 
            moves.Add(new Tuple<int, int>(takeRight.Item1 + x, takeRight.Item2 + y));
        if (Utilities.isValidLocation(takeLeft.Item1 + x, takeLeft.Item2 + y) && isValidMove(takeLeft.Item1 + x, takeLeft.Item2 + y, terminating))
            moves.Add(new Tuple<int, int>(takeLeft.Item1 + x, takeLeft.Item2 + y));
        return moves;
    }
}