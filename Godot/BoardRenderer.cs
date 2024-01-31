using Godot;
using ChessPuzzler;
using System;

public partial class BoardRenderer : Node2D
{
	[Export]
	public Label PuzzleLabel;
	[Export]
	public Label StatusLabel;
	[Export]
	public VBoxContainer TextContainer;

	public GameState GameState { get; } = DefaultGameState();
	public const float CellSize = 104f;
	public event Action OnStateChange;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitializeBoard();
	}

	public void InitializeBoard()
	{
		PuzzleLabel.Text = $"Puzzle #{GameState.PuzzleId}";
		TextContainer.Position = new Vector2(GameState.CurrentPuzzle.Board.Size * CellSize + 10, 0);
		UpdateStatus();
		// Remove previous nodes
		foreach (Node n in GetChildren())
		{
			n.QueueFree();
		}

		// Initialize new tiles
		OnStateChange = null;
		OnStateChange += UpdateStatus;
		Board board = GameState.CurrentPuzzle.Board;
		foreach (int rank in board.Ranks())
		{
			foreach (char file in board.Files())
			{
				BoardTile tile = new BoardTile();
				Position position = new Position(rank, file);
				tile.Position = new Vector2((file - 'a') * CellSize, (board.Size - rank) * CellSize);

				void RenderTile()
				{
					tile.Color = (position.Rank + position.File) % 2 == 0 ? Colors.DarkGray : Colors.Beige;

					if (position == GameState.SelectedPosition)
					{
						tile.Color = Colors.Orange;
					}
					else if (position == GameState.CursorPosition)
					{
						tile.Color = Colors.Yellow;
					}

					if (board.IsOccupied(position.Rank, position.File))
					{
						tile.Piece = board.GetPiece(position.Rank, position.File);
					}
					else
					{
						tile.Piece = null;
					}
				}

				OnStateChange += RenderTile;
				RenderTile();
				AddChild(tile);
			}
		}
	}

	public void UpdateStatus()
	{
		StatusLabel.Text = "Unsolved";
		if (GameState.CheatedPuzzles.ContainsKey(GameState.PuzzleId))
		{
			StatusLabel.Text = "Cheated";
		}
		else if (GameState.SolvedPuzzles.Contains(GameState.PuzzleId))
		{
			StatusLabel.Text = "Solved";
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is not InputEventKey eventKey) { return; }
		if (!eventKey.Pressed) { return; }
		Action action = eventKey.Keycode switch
		{
			Key.W => () => MoveCursor(1, 0),
			Key.S => () => MoveCursor(-1, 0),
			Key.A => () => MoveCursor(0, -1),
			Key.D => () => MoveCursor(0, 1),
			Key.Space => () => GameState.HandleSelect(),
			Key.N => () => { GameState.NextPuzzle(1); InitializeBoard(); },
			Key.P => () => { GameState.NextPuzzle(-1); InitializeBoard(); },
			Key.R => () => { GameState.CurrentPuzzle.Reset(); InitializeBoard(); },
			Key.Escape => () => GetTree().Quit(),
			_ => () => {}
		};
		action.Invoke();
		OnStateChange?.Invoke();
	}

	private void MoveCursor(int dRank, int dFile)
	{
		var (rank, file) = GameState.CursorPosition;
		GameState.CursorPosition = new Position(rank + dRank, (char)(file + dFile));
	}

	public static GameState DefaultGameState()
	{
		return new GameState(1, new GodotResourcePuzzleLibrary());
	}
}

public class GodotResourcePuzzleLibrary : IPuzzleLibrary
{
	public Puzzle LoadPuzzle(int id)
	{
		string[] data = FileAccess.GetFileAsString($"res://puzzles/{id.ToString().PadLeft(2, '0')}.txt").ReplaceLineEndings().Split(System.Environment.NewLine);
		Puzzle puzzle = new Puzzle(BoardReader.Read(data));
		return puzzle;
	}
}