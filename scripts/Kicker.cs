using Godot;
using System;
using System.Collections.Generic;

public class Kicker : RigidBody
{
	Random rand = new Random();
	private Area footArea;
	
	public float Speed {get; set; }
	public override void _Ready()
	{
		footArea = GetNode<Area>("FootArea");
		footArea.Connect("body_entered", this, "on_BodyEntered");
		Speed = ((EnemyTeam) GetParent()).Speed;
	}
	
	private Vector3 getRandomKickerPos()
	{
		List<RigidBody> kickers = ((EnemyTeam) GetParent()).Kickers;
		return kickers[rand.Next(kickers.Count)].GlobalTransform.origin;
	}
	
	private bool shouldShoot(Vector3 a, Vector3 b)
	{
		return (((EnemyTeam) GetParent()).canPass() || a.DistanceTo(b) < 10f);
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */
	
	private void on_BodyEntered(object body)
	{
		if (body is Ball)
		{
			((RigidBody) body).LinearVelocity = Vector3.Zero;
			((RigidBody) body).AngularVelocity = Vector3.Zero;
			
			Vector3 other = getRandomKickerPos();
			
			if (shouldShoot(other, ((RigidBody) body).GlobalTransform.origin))
				((RigidBody) body).Call("ShootAtGoal", Speed);
			else
				((RigidBody) body).Call("PassToPlayer", other, Speed);
		}
	}
}
