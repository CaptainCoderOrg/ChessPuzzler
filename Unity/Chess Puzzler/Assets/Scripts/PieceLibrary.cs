using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChessPuzzler;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceLibrary", menuName ="Puzzler/PieceLibrary")]
public class PieceLibrary : ScriptableObject
{
    public Entry[] Entries;
    public Sprite Lookup(ChessPiece piece) => Entries.Where(entry => entry.Piece == piece).First().Sprite;
}

[Serializable]
public class Entry
{
    public ChessPiece Piece;
    public Sprite Sprite;
}
