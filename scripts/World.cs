using Godot;
using System;

public class World : Spatial
{
	private Node SwipeDetector;
	private KinematicBody Player;
	private RigidBody Ball;
	private StaticBody Net;

	private Node GameTimers;
	
	private Area OutOfBoundsLeft;
	private Area OutOfBoundsRight;

	public override void _Ready()
	{
		GameTimers = GetNode<Node>("GameTimers");
		SwipeDetector = GetNode<Node>("SwipeDetector");
		Player = GetNode<KinematicBody>("Player");
		Ball = GetNode<RigidBody>("Ball");
		Net = GetNode<StaticBody>("Net");

		SwipeDetector.Connect("Swiped", Player, "on_Swiped");
		
		GameTimers.Connect("Reset", Ball, "on_Reset");
		GameTimers.Connect("Reset", Player, "on_Reset");
		GameTimers.Connect("Reset", Net, "on_Reset");

		Net.Connect("InGoalieZone", GameTimers, "on_InGoalieZone");
		
		OutOfBoundsLeft = GetNode<Area>("OutOfBoundsLeft");
		OutOfBoundsRight = GetNode<Area>("OutOfBoundsRight");
		
		/*
		OutOfBoundsLeft.Connect("body_entered", this, "on_OutOfBoundsLeft");
		OutOfBoundsRight.Connect("body_entered", this, "on_OutOfBoundsRight");
		*/
	}
	
	/*
	private void on_OutOfBoundsLeft(object body)
	{
		Transform t = Player.GlobalTransform;
		t.origin.x += 45f;
		Player.GlobalTransform = t;
	}
	
	private void on_OutOfBoundsRight(object body)
	{
		Transform t = Player.GlobalTransform;
		t.origin.x += -45f;
		Player.GlobalTransform = t;
	}
	*/
}
