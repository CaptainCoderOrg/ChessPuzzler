using ChessPuzzler;
using Godot;
using System.Collections.Generic;

public partial class BoardTile : Node2D
{
	private static Dictionary<ChessPiece, Texture2D> _textures = LoadTextures();
	private static Dictionary<ChessPiece, Texture2D> LoadTextures()
	{
		if (_textures is null)
		{
			_textures = new Dictionary<ChessPiece, Texture2D>();
			_textures[ChessPiece.Pawn] = ResourceLoader.Load<Texture2D>("res://sprites/pawn.png");
			_textures[ChessPiece.King] = ResourceLoader.Load<Texture2D>("res://sprites/king.png");
			_textures[ChessPiece.Knight] = ResourceLoader.Load<Texture2D>("res://sprites/knight.png");
			_textures[ChessPiece.Rook] = ResourceLoader.Load<Texture2D>("res://sprites/rook.png");
			_textures[ChessPiece.Queen] = ResourceLoader.Load<Texture2D>("res://sprites/queen.png");
			_textures[ChessPiece.Bishop] = ResourceLoader.Load<Texture2D>("res://sprites/bishop.png");
		}
		return _textures;
	}
	private Color _color;

	public Color Color 
	{ 
		get => _color; 
		set
		{
			_color = value;
			QueueRedraw();
		}
	}
	private ChessPiece? _piece;
	public ChessPiece? Piece 
	{ 
		get => _piece; 
		set
		{
			_piece = value;
			UpdateSprite();
		}
	}
	private Sprite2D _sprite;
	public override void _Draw()
	{
		Rect2 rect = new Rect2(0, 0, BoardRenderer.CellSize, BoardRenderer.CellSize);
		DrawRect(rect, Color, true);
	}

	private void UpdateSprite()
	{
		if (_sprite is null) { return; }
		if (_piece is null) 
		{
			_sprite.Hide();
		}
		else
		{
			_sprite.Texture = _textures[_piece.Value];
			_sprite.Show();
		}

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _sprite = new Sprite2D
        {
            Position = new Vector2(BoardRenderer.CellSize / 2, BoardRenderer.CellSize / 2)
			
        };
		_sprite.Scale = new Vector2(1.5f, 1.5f);
        UpdateSprite();
		AddChild(_sprite);
	}
}
