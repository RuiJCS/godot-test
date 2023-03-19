using Godot;
using System;

public partial class player : Area2D
{
	[Export]
	public int Speed = 400; // How fast the player will move (pixels/sec).

	public Vector2 ScreenSize; // Size of the game window.
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("move_right"))
		{
			velocity[0] += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity[0] -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity[1] += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity[1] -= 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		Position += velocity * (float)delta;
		Position = new Vector2(
  		x: Mathf.Clamp(Position[0], 0, ScreenSize[0]),
  		y: Mathf.Clamp(Position[1], 0, ScreenSize[1])
);
	}
}
