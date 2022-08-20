using Godot;
using System;

public class World : Spatial
{
	 private Node SwipeDetector;
	 private KinematicBody Player;
	 private RigidBody Ball;
	 private StaticBody Goal;

	 private Node GameTimers;
	 private CanvasLayer MenuOverlay;

	public override void _Ready()
	{
		GameTimers = GetNode<Node>("GameTimers");
		MenuOverlay = GetNode<CanvasLayer>("MenuOverlay");
		SwipeDetector = GetNode<Node>("SwipeDetector");
		Player = GetNode<KinematicBody>("Player");
		Ball = GetNode<RigidBody>("Ball");
		Goal = GetNode<StaticBody>("Goal");

		SwipeDetector.Connect("Swiped", Player, "on_Swiped");
		SwipeDetector.Connect("Held", Player, "on_Held");
		
		GameTimers.Connect("Shoot", Ball, "on_Shoot");
		GameTimers.Connect("Reset", Ball, "on_Reset");
		GameTimers.Connect("Reset", Player, "on_Reset");

		Goal.Connect("Missed", MenuOverlay, "on_UpdateScore");

		
	}
}
