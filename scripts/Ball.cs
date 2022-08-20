using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
	Random rand = new Random();
	float power = 35f;
	float horiz_max = 5f;
	float vert_max = 5f;
	
	Vector3 pos = new Vector3(0f, 2f, -90f);

	public override void _Ready()
	{
		on_Reset();
	} 
	
	private void straightShot()
	{
		float w1 = (float) ((rand.NextDouble() * 2) - 1f);
		float w2 = (float) ((rand.NextDouble() * 2) - 1f);
		
		Vector3 impulse = new Vector3(horiz_max * w1, vert_max * w2, power);
		this.ApplyImpulse(Vector3.Zero, impulse);
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */

	private void on_Shoot()
	{
		straightShot();
	}

	private void on_Reset()
	{
		this.AngularVelocity = Vector3.Zero;
		this.LinearVelocity = Vector3.Zero;

		Transform t = new Transform();
		t.origin = pos;
		t.basis = Basis.Identity;
		this.GlobalTransform = t;
	}
}
