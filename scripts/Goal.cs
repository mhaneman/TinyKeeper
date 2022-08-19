using Godot;
using System;

public class Goal : StaticBody
{
	[Signal] delegate void Missed();
	[Signal] delegate void Scored();

	private Area missed;
	private Area scored;
	public override void _Ready()
	{
		missed = GetNode<Area>("Missed");
		scored = GetNode<Area>("Scored");

		missed.Connect("body_entered", this, "on_Missed");
		scored.Connect("body_entered", this, "on_Scored");
	}

	private void on_Missed(object body)
	{
		EmitSignal("Missed");
	}

	private void on_Scored(object body)
	{
		EmitSignal("Scored");
	}
}
