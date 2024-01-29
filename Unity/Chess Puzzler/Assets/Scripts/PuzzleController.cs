using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessPuzzler;
using TMPro;

public class PuzzleController : MonoBehaviour
{
    public Color WhiteTile = new Color(211, 176, 131);
    public Color BlackTile = new Color(80, 80, 80);
    public Color CursorTile = new Color(253, 249, 0);
    public Color SelectedTile = new Color(255, 161, 0);

    public GridLayoutGroup Grid;
    public UnityPuzzleLibrary PuzzleLibrary;
    public PieceLibrary PieceLibrary;
    public GridButton BoardButtonPrefab;
    public TextMeshProUGUI PuzzleNumber;
    public TextMeshProUGUI PuzzleStatus;
    public TextMeshProUGUI PuzzleSolution;
    private GameState _gameState;

    void Awake()
    {
        _gameState = new GameState(1, PuzzleLibrary);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { MoveCursor(1, 0); }
        if (Input.GetKeyDown(KeyCode.S)) { MoveCursor(-1, 0); }
        if (Input.GetKeyDown(KeyCode.A)) { MoveCursor(0, -1); }
        if (Input.GetKeyDown(KeyCode.D)) { MoveCursor(0, 1); }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _gameState.HandleSelect();
            DrawBoard();
        }
        if (Input.GetKeyDown(KeyCode.R)) { Reset(); }
        if (Input.GetKeyDown(KeyCode.N)) { Next(); }
        if (Input.GetKeyDown(KeyCode.P)) { Prev(); }
        if (Input.GetKeyDown(KeyCode.Y)) { Cheat(); }
        PuzzleNumber.text = $"Puzzle #{_gameState.PuzzleId}";
        PuzzleSolution.gameObject.SetActive(false);
        if (_gameState.CheatedPuzzles.TryGetValue(_gameState.PuzzleId, out SolverResult solution))
        {
            PuzzleStatus.text = "Cheated!";
            PuzzleStatus.color = Color.yellow;
            PuzzleSolution.text = $"Cheater: ";
            foreach ((Position from, Position to) in solution.Moves)
            {
                PuzzleSolution.text += $"{from.File}{from.Rank}-{to.File}{to.Rank} ";
            }
            PuzzleSolution.gameObject.SetActive(true);
        }
        else if (_gameState.SolvedPuzzles.Contains(_gameState.PuzzleId))
        {
            PuzzleStatus.text = "Solved!";
            PuzzleStatus.color = Color.green;
        }
        else
        {
            PuzzleStatus.text = "Unsolved";
            PuzzleStatus.color = Color.white;
        }

    }

    public void Reset()
    {
        _gameState.CurrentPuzzle.Reset();
        DrawBoard();
    }
    public void Next()
    {
        _gameState.NextPuzzle(1);
        DrawBoard();
    }
    public void Prev()
    {

        _gameState.NextPuzzle(-1);
        DrawBoard();
    }
    public void Cheat()
    {
        _gameState.Cheat();
        DrawBoard();
    }

    private void MoveCursor(int dRank, int dFile)
    {
        var (rank, file) = _gameState.CursorPosition;
        Position newP = new Position(rank + dRank, (char)(file + dFile));
        _gameState.CursorPosition = newP;
        DrawBoard();
    }


    // Start is called before the first frame update
    void Start()
    {
        DrawBoard();
    }

    public void DrawBoard()
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
                GridButton cell = Instantiate(BoardButtonPrefab);
                // cell.Text.text = $"{file}{rank}";
                cell.Rank = rank;
                cell.File = file;
                cell.PieceImage.color = new Color(1f, 1f, 1f, 0f);
                cell.GridImage.color = CellColor(new Position(rank, file));
                cell.Button.onClick.AddListener(() => HandleClick(rank, file));
                if (board.IsOccupied(rank, file))
                {
                    cell.PieceImage.sprite = PieceLibrary.Lookup(board.GetPiece(rank, file));
                    cell.PieceImage.color = new Color(1f, 1f, 1f, 1f);
                }
                cell.transform.SetParent(Grid.transform);
                cell.transform.localScale = new Vector3(1,1,1);
            }
        }
    }
    private void HandleClick(int rank, char file)
    {
        _gameState.CursorPosition = new Position(rank, file);
        _gameState.HandleSelect();
        DrawBoard();
    }

    private Color CellColor(Position position)
    {
        if (_gameState.SelectedPosition == position) { return SelectedTile; }
        if (_gameState.CursorPosition == position) { return CursorTile; }
        if ((position.Rank + position.File) % 2 == 0) { return WhiteTile; }
        return BlackTile;
    }
}
