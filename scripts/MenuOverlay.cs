using Godot;
using System;

public class MenuOverlay : CanvasLayer
{
	int score = 0;

	private Label scoreLabel;
	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("Score");
	}

	private void on_UpdateScore()
	{
		score++;
		scoreLabel.Text = "Score: " + score.ToString();
	}
}
