using Godot;
using System;

public class ScaleableCamera : Spatial
{
	private Camera camera;
	
	private Vector2 screen_dim;
	private float intended_screen_ratio = 1.5f;
	private float screen_ratio;
	private float scaling_ratio;
	public override void _Ready()
	{
		camera = GetNode<Camera>("Camera");
		
		screen_dim = OS.WindowSize;
		screen_ratio = screen_dim.x / screen_dim.y;
		scaling_ratio = intended_screen_ratio / screen_ratio;
		// camera.Fov *= scaling_ratio;
	}
	
	
}
