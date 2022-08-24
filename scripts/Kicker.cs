using Godot;
using System;
using System.Collections.Generic;

public class Kicker : RigidBody
{
	Random rand = new Random();
	private Area footArea;
	public override void _Ready()
	{
		footArea = GetNode<Area>("FootArea");
		footArea.Connect("body_entered", this, "on_BodyEntered");
	}
	
	private Vector3 getRandomKickerPos()
	{
		List<RigidBody> kickers = ((EnemyTeam) GetParent()).Kickers;
		return kickers[rand.Next(kickers.Count)].GlobalTransform.origin;
	}
	
	private bool shouldShoot(Vector3 a, Vector3 b)
	{
		return a.DistanceTo(b) < 10f;
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */
	
	private void on_BodyEntered(object body)
	{
		if (body is RigidBody && ((RigidBody) body).Name == "Ball")
		{
			((RigidBody) body).LinearVelocity = Vector3.Zero;
			((RigidBody) body).AngularVelocity = Vector3.Zero;
			
			Vector3 other = getRandomKickerPos();
			
			if (shouldShoot(other, ((RigidBody) body).GlobalTransform.origin))
				((RigidBody) body).Call("ShootAtGoal");
			else
				((RigidBody) body).Call("PassToPlayer", other);
		}
	}
}
