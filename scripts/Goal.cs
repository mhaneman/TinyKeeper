using Godot;
using System;

public class Goal : StaticBody
{
	[Signal] delegate void Missed();
	[Signal] delegate void Scored();

	private Area missed;
	private Area scored;

	private PackedScene Cross;
	private PackedScene Circle;
	public override void _Ready()
	{
		Cross = (PackedScene) ResourceLoader.Load("res://scenes/Items/Cross.tscn");
		Circle = (PackedScene) ResourceLoader.Load("res://scenes/Items/Circle.tscn");

		missed = GetNode<Area>("Missed");
		scored = GetNode<Area>("Scored");

		missed.Connect("body_entered", this, "on_Missed");
		scored.Connect("body_entered", this, "on_Scored");
	}

	private void on_Missed(object body)
	{
		MeshInstance newCircle = (MeshInstance) Circle.Instance();
		Spatial n = (Spatial) body;
		Transform loc = n.GlobalTransform;
		newCircle.GlobalTransform = loc;
		AddChild(newCircle);
		EmitSignal("Missed");
	}

	private void on_Scored(object body)
	{
		MeshInstance newCross = (MeshInstance) Cross.Instance();
		Spatial n = (Spatial) body;
		Transform loc = n.GlobalTransform;
		newCross.GlobalTransform = loc;
		AddChild(newCross);
		EmitSignal("Scored");
	}
}
