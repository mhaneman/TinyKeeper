using Godot;
using System;
using System.Collections.Generic;

public class EnemyTeam : Spatial
{
	public List<RigidBody> Kickers {get; set; }
	public override void _Ready()
	{
		Kickers = new List<RigidBody>();
		InstanceNewKicker(new Vector3(40f, 0f, -40f));
		InstanceNewKicker(new Vector3(-40f, 0f, -40f));
		InstanceNewKicker(new Vector3(0f, 0f, -80f));
	}
	
	private void InstanceNewKicker(Vector3 pos)
	{
		PackedScene scene = (PackedScene) ResourceLoader.Load("res://scenes/Characters/Kicker.tscn");
		RigidBody newKicker = (RigidBody) scene.Instance();
		newKicker.GlobalTransform = fieldPosition(pos);
		Kickers.Add(newKicker);
		this.CallDeferred("add_child", newKicker);
	}
	
	private Transform fieldPosition(Vector3 pos)
	{
		Transform t = new Transform();
		t.origin = pos;
		t.basis = Basis.Identity	;
		return t;
	}
}
