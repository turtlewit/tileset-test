using Godot;
using System;

public class Player : KinematicBody2D
{
    const string MOVE_LEFT = "player_move_left";
    const string MOVE_RIGHT = "player_move_right";
    const string JUMP = "player_move_jump";

    [Export]
    Vector2 gravity = new Vector2(0, 100);

    [Export]
    float speed = 50;

    ushort floor_bodies_colliding = 0;
    Vector2 linear_velocity = new Vector2(0, 0);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        if (floor_bodies_colliding < 1) {
            Fall(delta);
            linear_velocity = MoveAndSlideWithSnap(linear_velocity, snap: new Vector2(0, 0), floorNormal: new Vector2(0, -1));
        } else {
            FloorMove(delta);
            MoveAndSlideWithSnap(linear_velocity, snap: new Vector2(0, 16f), floorNormal: new Vector2(0, -1), floorMaxAngle: 45f);
        }
    }

    private void Fall(float delta)
    {
        linear_velocity += gravity * delta;
    }

    private void FloorMove(float delta)
    {
        float dir = Input.GetActionStrength(MOVE_RIGHT)
                      - Input.GetActionStrength(MOVE_LEFT);
        linear_velocity = new Vector2(dir * speed, 0);
    }

    public void _on_FloorDetection_body_entered(PhysicsBody2D b)
    {
        floor_bodies_colliding += 1;
    }

    public void _on_FloorDetection_body_exited(PhysicsBody2D b)
    {
        floor_bodies_colliding -= 1;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
