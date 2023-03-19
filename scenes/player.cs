using Godot;
using System;

public partial class player : Area2D
{
	[Export]
	public int Speed = 400; // How fast the player will move (pixels/sec).
		
	[Signal]
	public delegate void HitEventHandler();

	public Vector2 ScreenSize; // Size of the game window.
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
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

	private void _on_body_entered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Hit);
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}
