using Godot;
using System;
using System.Collections.Generic;

public abstract class Player : Area2D
{
    protected Vector2 _screenSize;
    protected Vector2 _velocity;
    [Export] public int Speed { get; private set; }  = 100;
    [Export] public Vector2 StartPosition { get; private set; }
    [Export] public PlayerName PlayerName { get; private set; }
    
    public int Score { get; private set; }

    [Signal] public delegate void Pontua(Player player);

    public override void _Ready(){
        Score = 0;
        _screenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta){
        _velocity = new Vector2();

        OnJoystick();
        AnimPlayer();
        SetVelocity(delta);
        OnScreenExited();

    }

    public void OnBodyEntered(RigidBody2D body){
        if (body.Name == "Ganha"){
            EmitSignal("Pontua", this);
        }else{
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        }
        Position = StartPosition;
    }

    private void OnJoystick(){
        if(PlayerName == PlayerName.PLAYER_1){
            if (Input.IsActionPressed("ui_down")){
                _velocity.y += 1;
            }
            if (Input.IsActionPressed("ui_up")){
                _velocity.y -= 1;
            }
        }else if(PlayerName == PlayerName.PLAYER_2){
            if (Input.IsActionPressed("desce")){
                _velocity.y += 1;
            }
            if (Input.IsActionPressed("sobe")){
                _velocity.y -= 1;
            }
        }
    }

    public void Pontuar(){
        Score++;
    }

    private void SetVelocity(float delta){
        Position += _velocity * delta;
    }

    private void OnScreenExited(){
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }

    private void AnimPlayer(){
        if (_velocity.Length() > 0){
            _velocity = _velocity.Normalized() * Speed;
            GetNode<AnimatedSprite>("AnimatedSprite").Play();
        }else{
            GetNode<AnimatedSprite>("AnimatedSprite").Stop();
        }

        //Animações
        if (_velocity.y > 0){
            GetNode<AnimatedSprite>("AnimatedSprite").Play("baixo");
        }else{
            GetNode<AnimatedSprite>("AnimatedSprite").Play("cima");
        }
    }
    public void ResetPosition(){
        Position = StartPosition;
    }
    public void ResetScore(){
        Score = 0;
    }
}