using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessPuzzler;

[CreateAssetMenu(fileName ="PuzzleLibrary", menuName ="Puzzler/PuzzleLibrary")]
public class UnityPuzzleLibrary : ScriptableObject, IPuzzleLibrary
{
    public TextAsset[] Puzzles;
    public Puzzle LoadPuzzle(int id)
    {
        return new Puzzle(BoardReader.Read(Puzzles[id].text.Split()));
    }
}
