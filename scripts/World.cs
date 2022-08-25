using Godot;
using System;

public class World : Spatial
{
	private Node SwipeDetector;
	private KinematicBody Player;
	private RigidBody Ball;
	private StaticBody Net;
	private Spatial ScoreBoard;
	private Spatial EnemyTeam;

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
		EnemyTeam = GetNode<Spatial>("EnemyTeam");

		SwipeDetector.Connect("Swiped", Player, "on_Swiped");
		
		GameTimers.Connect("Reset", Ball, "on_Reset");
		GameTimers.Connect("Reset", Player, "on_Reset");
		GameTimers.Connect("Reset", Net, "on_Reset");
		GameTimers.Connect("Reset", EnemyTeam, "on_Reset");
		

		Net.Connect("InGoalieZone", GameTimers, "on_InGoalieZone");
		Net.Connect("Scored", ScoreBoard, "on_Scored");
		
		OutOfBoundsLeft = GetNode<Area>("OutOfBoundsLeft");
		OutOfBoundsRight = GetNode<Area>("OutOfBoundsRight");
		
		OutOfBoundsLeft.Connect("body_entered", this, "on_OutOfBoundsLeft");
		OutOfBoundsRight.Connect("body_entered", this, "on_OutOfBoundsRight");
		
		float ratio = phoneScaleRatio();
		Transform t = OutOfBoundsLeft.GlobalTransform;
		t.origin *= Vector3.Right * ratio;
		OutOfBoundsLeft.GlobalTransform = t;
		
		t = OutOfBoundsRight.GlobalTransform;
		t.origin *= Vector3.Right * ratio;
		OutOfBoundsRight.GlobalTransform = t;
	}
	
	private float phoneScaleRatio()
	{
		float intended_screen_ratio = 1.5f;
		Vector2 screen_dim = OS.WindowSize;
		float screen_ratio = screen_dim.x / screen_dim.y;
		return screen_ratio / intended_screen_ratio;
	}
	
	/* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
	/* Signals */
	
	private void on_OutOfBoundsLeft(object body)
	{
		Transform t = Player.GlobalTransform;
		t.origin.x = 15f;
		Player.GlobalTransform = t;
	}
	
	private void on_OutOfBoundsRight(object body)
	{
		Transform t = Player.GlobalTransform;
		t.origin.x = -15f;
		Player.GlobalTransform = t;
	}
}
