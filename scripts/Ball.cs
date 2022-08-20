using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
	Random rand = new Random();
	float power = 35f;
	float horiz_max = 5f;
	float vert_max = 5f;
	
	float mag_power = 0f;
	Vector3 dir = Vector3.Zero;
	
	Vector3 pos = new Vector3(0f, 2f, -90f);

	private Particles SpeedParticles;
	public override void _Ready()
	{
		SpeedParticles = GetNode<Particles>("SpeedParticles");
		on_Reset();
	} 
	
	public override void _PhysicsProcess(float delta)
	{
		if (this.LinearVelocity.Length() > 40f)
			SpeedParticles.Emitting = true;
		else 
			SpeedParticles.Emitting = false;
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState state)
	{
		if (this.LinearVelocity.Length() > 10f && 
			this.LinearVelocity.z > 0f &&
			mag_power > 0f)
		{
			var lin_vel = state.LinearVelocity.Normalized();
			var magnus = lin_vel.Rotated(dir, 1.57f) * mag_power;
			this.AddForce(magnus, magnus);
		}
	}
	
	private void setMagnus(Vector3 direction, float power)
	{
		this.dir = direction;
		this.mag_power = power;
	}
	
	private void curveShot(Vector3 impulse, Vector3 direction, float mag_power)
	{	
		this.ApplyImpulse(Vector3.Zero, impulse);
		setMagnus(direction, mag_power);
	}
	
	private void straightShot()
	{
		float w1 = (float) ((rand.NextDouble() * 2) - 1f);
		float w2 = (float) ((rand.NextDouble() * 2) - 1f);
		
		Vector3 impulse = new Vector3(horiz_max * w1, vert_max * w2, power);
		this.ApplyImpulse(Vector3.Zero, impulse);
		setMagnus(Vector3.Zero, 0f);
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */

	private void on_Shoot()
	{
		float shot = (float) rand.NextDouble();
		if (shot < 0.3f)
			curveShot(new Vector3(-horiz_max * 1.5f, vert_max * 0.75f, power), Vector3.Up, 27f);
		else if (shot < 0.6f)
			curveShot(new Vector3(horiz_max * 1.5f, vert_max * 0.75f, power), Vector3.Down, 27f);
		else
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
