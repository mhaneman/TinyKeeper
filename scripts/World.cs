using Godot;
using System;

public class World : Spatial
{
	 private Node SwipeDetector;
	 private KinematicBody Player;
	 private RigidBody Ball;
	 private StaticBody Net;

	 private Node GameTimers;
	 private CanvasLayer MenuOverlay;

	public override void _Ready()
	{
		GameTimers = GetNode<Node>("GameTimers");
		MenuOverlay = GetNode<CanvasLayer>("MenuOverlay");
		SwipeDetector = GetNode<Node>("SwipeDetector");
		Player = GetNode<KinematicBody>("Player");
		Ball = GetNode<RigidBody>("Ball");
		Net = GetNode<StaticBody>("Net");

		SwipeDetector.Connect("Swiped", Player, "on_Swiped");
		
		GameTimers.Connect("Shoot", Ball, "on_Shoot");
		GameTimers.Connect("Reset", Ball, "on_Reset");
		GameTimers.Connect("Reset", Player, "on_Reset");

		Net.Connect("Missed", MenuOverlay, "on_UpdateScore");

		
	}
}
