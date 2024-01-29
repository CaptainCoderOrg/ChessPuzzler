using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessPuzzler;

public class PuzzleController : MonoBehaviour
{
    public GridLayoutGroup Grid;
    public UnityPuzzleLibrary PuzzleLibrary;
    public PieceLibrary PieceLibrary;
    public GridButton BoardButtonPrefab;
    private GameState _gameState;

    void Awake()
    {
        _gameState = new GameState(1, PuzzleLibrary);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
    }

    public void InitBoard()
    {
        foreach (Transform child in Grid.transform)
        {
            Destroy(child.gameObject);
        }
        Board board = _gameState.CurrentPuzzle.Board;
        foreach (int rank in board.Ranks())
        {
            foreach (char file in board.Files())
            {
                GridButton button = Instantiate(BoardButtonPrefab);
                button.Text.text = $"{file}{rank}";
                if (board.IsOccupied(rank, file))
                {
                    button.Image.sprite = PieceLibrary.Lookup(board.GetPiece(rank, file));
                }
                button.transform.SetParent(Grid.transform);
            }
        }
    }
}
