using Godot;
using System;

public class BallShadowRayCast : RayCast
{

    private Sprite3D Shadow;
    private Vector3 offset = new Vector3(0f, 0.02f, 0f);
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
            float dist = this.GlobalTransform.origin.DistanceTo(newPoint) - 1.5f;

            Shadow.Scale = Vector3.One * scale(dist);

            position.origin = newPoint + offset;
            Shadow.GlobalTransform = position;

            if (dist < 0.22f)
                Shadow.Visible = false;
            else
                Shadow.Visible = true;
        }
    }

    private float scale(float dist)
    {
        float max_scale = 3f;
        float focal_point = 20f;

        float numerator = max_scale * (dist - focal_point);
        return -Mathf.Abs(numerator / focal_point) + max_scale;
    }
}
