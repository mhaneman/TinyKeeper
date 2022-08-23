using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
	Random rand = new Random();
	
	Vector3 inital_pos;
	
	float shot_power = 0f;
	float mag_power = 0f;
	Vector3 shot_dir = Vector3.Zero;
	Vector3 mag_dir = Vector3.Zero;

	private Particles SpeedParticles;
	public override void _Ready()
	{
		this.Connect("body_entered", this, "on_BodyEntered");
		inital_pos = this.GlobalTransform.origin;
		SpeedParticles = GetNode<Particles>("SpeedParticles");
		on_Reset();
	} 
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Physics */
	
	public override void _PhysicsProcess(float delta)
	{
		if (this.LinearVelocity.Length() > 40f)
			SpeedParticles.Visible = true;
		else 
			SpeedParticles.Visible = false;
		
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState state)
	{
		if (this.LinearVelocity.Length() > 10f && mag_power > 0f)
		{
			var lin_vel = state.LinearVelocity.Normalized();
			var magnus = lin_vel.Rotated(mag_dir, 1.57f) * mag_power;
			this.AddForce(magnus, magnus);
		}
	}
	
	private void setMagnus(Vector3 direction, float power)
	{
		this.mag_dir = direction;
		this.mag_power = power;
	}

	private void setShot(Vector3 direction, float power)
	{
		Vector3 impulse = new Vector3(direction.x * power, direction.y * power, direction.z * power);
		this.ApplyImpulse(Vector3.Zero, impulse);
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */

	private void on_BodyEntered(object body)
	{
		if (this.LinearVelocity.x <= 0.01f && this.LinearVelocity.z <= 0.01f)
			setMagnus(Vector3.Zero, 0f);
	}

	private void on_Shoot()
	{
		float w1 = (float) rand.NextDouble() * 0.4f - 0.2f;
		float w2 = (float) rand.NextDouble() * 0.4f;

		Vector3 dir = new Vector3(w1, w2, 1f).Normalized();
		float power = 20f;

		setShot(dir, power);

		if (Mathf.Abs(w1) < 0.1f && w2 > 0.08f)
		{
			if (w1 < 0)
				setMagnus(Vector3.Down, 5f);
			else
				setMagnus(Vector3.Up, 5f);
		}
		else if (w2 > 0.25) 
		{
			setMagnus(Vector3.Right, 16f);
		}
		GD.Print("Weight 1: ", w1);
		GD.Print("Weight 2: ", w2);
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
