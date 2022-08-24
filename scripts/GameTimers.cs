using Godot;
using System;

public class GameTimers : Node
{
	[Signal] delegate void Reset();
	private Timer reset;

	public override void _Ready()
	{
		reset = GetNode<Timer>("Reset");
		reset.Connect("timeout", this, "on_reset_timeout");
	}
	
	private void on_InGoalieZone()
	{
		reset.Start();
	}

	private void on_reset_timeout()
	{
		EmitSignal("Reset");
	}
}
