using Godot;
using System;

public class PlayerShadowRayCast : RayCast
{

	private Sprite3D Shadow;
	private Vector3 offset = new Vector3(0f, 0.01f, 0f);
	Transform position = new Transform();
	public override void _Ready()
	{
		Shadow = GetNode<Sprite3D>("Shadow");
		Shadow.Visible = false;

		position.origin = Vector3.Zero;
		position.basis = Shadow.Transform.basis;
	}


	public override void _Process(float delta)
	{
		if (this.IsColliding())
		{
			Vector3 newPoint = GetCollisionPoint();
			position.origin = newPoint + offset;
			Shadow.GlobalTransform = position;
			Shadow.Visible = true;
		} 
		else
			Shadow.Visible = false;

	}
}
