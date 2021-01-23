using Godot;
using System;

public class Player2 : Area2D
{
    [Signal] public delegate void Pontua2();
    [Export] public int Speed = 100;

    private Vector2 _screenSize;
    public override void _Ready()
    {
        _screenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        var velocity = new Vector2();

        if(Input.IsActionPressed("desce")){
            velocity.y += 1;
        }
        if(Input.IsActionPressed("sobe")){
            velocity.y -= 1;
        }

        if(velocity.Length() > 0){
            velocity = velocity.Normalized() * Speed;
            GetNode<AnimatedSprite>("AnimatedSprite").Play();
        }else{
            GetNode<AnimatedSprite>("AnimatedSprite").Stop();
        }

        Position += velocity * delta;

        //Não sair da tela
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );

        //Animações
        if(velocity.y > 0){
            GetNode<AnimatedSprite>("AnimatedSprite").Play("baixo");
        }else{
            GetNode<AnimatedSprite>("AnimatedSprite").Play("cima");
        }
    }

    public void OnBodyEntered(RigidBody2D body){
        if (body.Name == "Ganha"){
            EmitSignal("Pontua2");
        }else{
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        }

        Position = new Vector2(
            x: 944,
            y: 696
        );
    }

    public void VoltaPos(){
        Position = new Vector2(
            x: 944,
            y: 696
        );
    }
}
