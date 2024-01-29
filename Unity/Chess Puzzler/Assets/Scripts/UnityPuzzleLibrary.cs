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
        id = id - 1;
        string rawText = Puzzles[id].text.Replace("\r\n", "\n");
        string[] data = Puzzles[id].text.Split("\n");
        // Debug.Log(string.Join("\n", data));
        return new Puzzle(BoardReader.Read(data));
    }
}
