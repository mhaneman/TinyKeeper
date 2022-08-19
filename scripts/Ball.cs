using Godot;
using System;
using System.Collections.Generic;

public class Ball : RigidBody
{
    Random rand = new Random();
    Vector3 pos = Vector3.Zero;
    List<Vector3> impulses = new List<Vector3>();
    float power = 50f;
    float horiz_max = 5f;
    float vert_max = 5f;

    public override void _Ready()
    {
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
        this.ApplyImpulse(pos, impulses[rand.Next(impulses.Count - 1)]);
        GD.Print("shoot");
    }

    private void on_Reset()
    {
        this.AngularVelocity = Vector3.Zero;
        this.LinearVelocity = Vector3.Zero;

        Transform t = new Transform();
        t.origin = new Vector3(0f, 2f, -140f);
        t.basis = Basis.Identity;
        this.GlobalTransform = t;
        
        GD.Print("reset");
    }
}
