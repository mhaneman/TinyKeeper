using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
	Random rand = new Random();
	List<Vector3> impulses = new List<Vector3>();
	float power = 35f;
	float horiz_max = 6f;
	float vert_max = 6f;
	
	Vector3 pos = new Vector3(0f, 2f, -90f);

	public override void _Ready()
	{
		on_Reset();
		// straight shots
		impulses.Add(new Vector3(0f, vert_max, power));
		impulses.Add(new Vector3(-horiz_max, 0f, power));
		impulses.Add(new Vector3(horiz_max, 0f, power));
		impulses.Add(new Vector3(-horiz_max, vert_max, power));
		impulses.Add(new Vector3(horiz_max, vert_max, power));
	} 
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */

	private void on_Shoot()
	{
		this.ApplyImpulse(Vector3.Zero, impulses[rand.Next(impulses.Count - 1)]);
		GD.Print("shoot");
	}

	private void on_Reset()
	{
		this.AngularVelocity = Vector3.Zero;
		this.LinearVelocity = Vector3.Zero;

		Transform t = new Transform();
		t.origin = pos;
		t.basis = Basis.Identity;
		this.GlobalTransform = t;
		
		GD.Print("reset");
	}
}
