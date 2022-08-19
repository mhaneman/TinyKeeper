using Godot;
using System;

public class Player : KinematicBody
{
    private float gravity = 1f;
    private float maxTermVel = 20f;
    private float jumpPowerVirt = 40f;
    private float jumpPowerHoriz = 40f;
    private float Friction = 0.2f;

    private Vector3 velocity;

    /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    /* Physics */

    public override void _PhysicsProcess(float delta)
    {
        applyPhysics();
        gravityPhysics();
    }

    private void gravityPhysics()
    {
        if (IsOnFloor())
        {
            velocity.x = Mathf.Lerp(velocity.x, 0, Friction);
            velocity.y = 0;	
        } 
		else
			velocity.y = Mathf.Clamp(velocity.y-gravity, -maxTermVel, jumpPowerHoriz);
    }

    private void applyPhysics()
    {
        velocity = MoveAndSlide(velocity, Vector3.Up);
    }



    /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    /* Signals */

    private void on_Swiped(Vector2 dir)
    {
        velocity.y = -dir.y * jumpPowerVirt;
        velocity.x = dir.x * jumpPowerHoriz;

        if (dir.x > 0.5f)
            this.RotateZ(1.57f);

        if (dir.x < -0.5f)
            this.RotateZ(-1.57f);
    }

    private void on_Reset()
    {
        this.velocity = Vector3.Zero;

        Transform t = new Transform();
        t.origin = new Vector3(0f, 1f, 0f);
        t.basis = Basis.Identity;
        this.GlobalTransform = t;
    }
}
