using Godot;
using System;

public class GameTimers : Node
{
	[Signal] delegate void Shoot();
	[Signal] delegate void Reset();

	private Timer shoot;
	private Timer reset;

	public override void _Ready()
	{
		shoot = GetNode<Timer>("Shoot");
		reset = GetNode<Timer>("Reset");

		shoot.Connect("timeout", this, "on_shoot_timeout");
		reset.Connect("timeout", this, "on_reset_timeout");
	}

	private void on_shoot_timeout()
	{
		GD.Print("GameTimer shoot");
		EmitSignal("Shoot");
	}

	private void on_reset_timeout()
	{
		GD.Print("GameTimer reset");
		shoot.Start();
		EmitSignal("Reset");
	}
}
