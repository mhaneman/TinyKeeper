using Godot;
using System;
using System.Collections.Generic;

public class EnemyTeam : Spatial
{
	public List<RigidBody> Kickers {get; set; }
	
	// max passes should be 1 but make it 50/50 for 1 and 2
	public int curr_passes = 0;
	public int max_passes = 2;
	
	public float Speed = 0.38f;
	private float SpeedMax = 0.5f;
	
	public override void _Ready()
	{
		Kickers = new List<RigidBody>();
		InstanceNewKicker(new Vector3(55f, 0f, -35f));
		InstanceNewKicker(new Vector3(-55f, 0f, -35f));
		
		InstanceNewKicker(new Vector3(0f, 0f, -80f));
		
		InstanceNewKicker(new Vector3(70f, 0f, -100f));
		InstanceNewKicker(new Vector3(-70f, 0f, -100f));
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
	
	public bool canPass()
	{
		return curr_passes++ >= max_passes;
	}
	
	private void on_Reset()
	{
		curr_passes = 0;
		if (Speed <= SpeedMax)
			Speed += 0.005f;
		
		foreach (Kicker i in Kickers)
			i.Speed = this.Speed;
	}
}
