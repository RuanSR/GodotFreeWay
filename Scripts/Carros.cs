using Godot;
using System;

public class Carros : RigidBody2D
{
    private Random _rand = new Random();
    public override void _Ready()
    {
       var carros = GetNode<AnimatedSprite>("AnimatedSprite");
       var carFrames = carros.Frames.GetAnimationNames();
       carros.Animation = carFrames[_rand.Next(0, carFrames.Length)];
    }

    public void OnVisibilityNotifierScreenExited(){
        QueueFree();
    }
}
