using Godot;
using System;

public class ScoreBoard : Spatial
{
    private Label3D otherScore;
    private Label3D myScore;
    public override void _Ready()
    {
        otherScore = GetNode<Label3D>("score_board/OtherScore");
        myScore = GetNode<Label3D>("score_board/MyScore");

        otherScore.Text = "0";
        myScore.Text = "3";
    }

    private void on_Scored()
    {
        int raw = Int16.Parse(otherScore.Text) + 1;
        otherScore.Text = raw.ToString();
        GD.Print("yep");

    }
}
