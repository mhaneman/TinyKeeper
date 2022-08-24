using Godot;
using System;

public class Goal : StaticBody
{
	[Signal] delegate void Missed();
	[Signal] delegate void Scored();
	[Signal] delegate void InGoalieZone();

	private Area missed;
	private Area scored;
	private Area goalieZone;

	private PackedScene CrossScene;
	private PackedScene CircleScene;

	private MeshInstance Cross;
	private MeshInstance Circle;
	public override void _Ready()
	{
		CrossScene = (PackedScene) ResourceLoader.Load("res://scenes/Items/Cross.tscn");
		Cross = (MeshInstance) CrossScene.Instance();
		this.CallDeferred("add_child", Cross);
		Cross.Visible = false;

		CircleScene = (PackedScene) ResourceLoader.Load("res://scenes/Items/Circle.tscn");
		Circle = (MeshInstance) CircleScene.Instance();
		this.CallDeferred("add_child", Circle);
		Circle.Visible = false;

		missed = GetNode<Area>("Missed");
		scored = GetNode<Area>("Scored");
		goalieZone = GetNode<Area>("GoalieZone");

		missed.Connect("body_entered", this, "on_Missed");
		scored.Connect("body_entered", this, "on_Scored");
		goalieZone.Connect("body_entered", this, "on_GoalieZone");
	}

	private Transform getLoc(object body)
	{
		Spatial n = (Spatial) body;
		Transform loc = n.GlobalTransform;
		loc.basis = Basis.Identity;
		return loc;
	}

	private void on_Missed(object body)
	{
		Transform loc = getLoc(body);
		Circle.GlobalTransform = loc;
		Circle.Visible = true;
		EmitSignal("Missed");
	}

	private void on_Scored(object body)
	{
		Transform loc = getLoc(body);
		Cross.GlobalTransform = loc;
		Cross.Visible = true;
		EmitSignal("Scored");
	}
	
	private void on_GoalieZone(object body)
	{
		EmitSignal("InGoalieZone");
	}

	private void on_Reset()
	{
		Cross.Visible = false;
		Circle.Visible = false;
	}
}
