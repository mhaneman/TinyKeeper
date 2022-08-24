using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
	Random rand = new Random();
	
	Vector3 inital_pos;


	public override void _Ready()
	{
		this.Connect("body_entered", this, "on_BodyEntered");
		inital_pos = this.GlobalTransform.origin;
		on_Reset();
	}
	
	public void ShootAtGoal()
	{
		Vector3 curr_pos = this.GlobalTransform.origin;
		
		float dist = curr_pos.DistanceTo(Vector3.Zero);
		float power = Mathf.Sqrt(dist) * 2.5f;
		
		Vector3 left = curr_pos.DirectionTo(new Vector3 (-16, 12, 0));
		Vector3 right = curr_pos.DirectionTo(new Vector3 (16, 12, 0));
		float w1 = (float) rand.NextDouble() * 0.1f;
		float w2 = (float) rand.NextDouble() * 0.2f;
		
		
		Vector3 dir;
		if (rand.Next(2) == 0)
			dir = new Vector3(left.x + w1, left.y + w2, left.z);
		else 
			dir = new Vector3(right.x - w1, right.y + w2, right.z);
		
		Vector3 impulse = new Vector3(dir.x * power, dir.y * power, dir.z * power);
		Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
		this.ApplyImpulse(offset, impulse);
	}
	
	public void PassToPlayer(Vector3 otherPlayer)
	{
		float dist = this.GlobalTransform.origin.DistanceTo(otherPlayer);
		float power = Mathf.Sqrt(dist) * 2f;
		Vector3 curr_pos = this.GlobalTransform.origin;
		Vector3 dir = curr_pos.DirectionTo(otherPlayer);
		dir.y = 0f;
		
		Vector3 impulse = new Vector3(dir.x * power, dir.y * power, dir.z * power);
		this.ApplyCentralImpulse(impulse);
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Physics */
	
	public override void _PhysicsProcess(float delta)
	{	
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */

	private void on_BodyEntered(object body)
	{
	}

	private void on_Reset()
	{
		this.AngularVelocity = Vector3.Zero;
		this.LinearVelocity = Vector3.Zero;

		Transform t = new Transform();
		t.origin = inital_pos;
		t.basis = Basis.Identity;
		this.GlobalTransform = t;
	}
}
