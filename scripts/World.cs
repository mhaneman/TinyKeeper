using Godot;
using System;

public class World : Spatial
{
	private Node SwipeDetector;
	private KinematicBody Player;
	private RigidBody Ball;
	private StaticBody Net;
	private Spatial ScoreBoard;

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
		ScoreBoard = GetNode<Spatial>("ScoreBoard");

		SwipeDetector.Connect("Swiped", Player, "on_Swiped");
		
		GameTimers.Connect("Reset", Ball, "on_Reset");
		GameTimers.Connect("Reset", Player, "on_Reset");
		GameTimers.Connect("Reset", Net, "on_Reset");

		Net.Connect("InGoalieZone", GameTimers, "on_InGoalieZone");
		Net.Connect("Scored", ScoreBoard, "on_Scored");
		
		OutOfBoundsLeft = GetNode<Area>("OutOfBoundsLeft");
		OutOfBoundsRight = GetNode<Area>("OutOfBoundsRight");
		
	}
}
