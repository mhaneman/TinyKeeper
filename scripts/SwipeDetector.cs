using Godot;
using System;

public class SwipeDetector : Node
{
	[Signal] delegate void Swiped(Vector2 dir);
	private Vector2 start_pos;
	private bool can_detect = false;
	private float detect_radius = 100f;

	public override void _UnhandledInput(InputEvent @event) {
		if (@event is InputEventScreenTouch eventKey) 
		{
			if (eventKey.Pressed)
			{
				start_pos = eventKey.Position;
				can_detect = true;
			}
		}
	
	}
	
	public override void _Process(float delta)
	{
		if (can_detect)
		{
			Vector2 cur_pos = GetViewport().GetMousePosition();
			float displ = start_pos.DistanceTo(cur_pos);
			
			if (displ >= detect_radius)
			{
				// then figure out what direction that is and send signal
                Vector2 dir = (cur_pos - start_pos).Normalized();
				EmitSignal("Swiped", dir);
				can_detect = false;	
			}	
		}
	}
}

