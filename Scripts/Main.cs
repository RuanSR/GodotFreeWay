using Godot;
using System;

public class Main : Node
{
    private Random _rand = new Random();
    private PackedScene _novoCarro = ResourceLoader.Load<PackedScene>("res://Scenes/Carros.tscn");
    private int[] _pdevagar = {600, 544, 324, 384, 216, 160};
    private int[] _prapido = {488, 272, 104};

    private int _score1, _score2 = 0;
    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("Tema").Play();
        GetNode<Button>("Button").Hide();
    }

    public void OnTimerCarroRapidoTimeOut(){
        var carroRapido = (Carros)_novoCarro.Instance();
        AddChild(carroRapido);
        carroRapido.Position = new Vector2(
            x: - 10,
            y: _prapido[_rand.Next(0, _prapido.Length)]
        );
        carroRapido.LinearVelocity = new Vector2(
            x: _rand.Next(700, 725),
            y: 0
        );
    }

    public void OnTimerCarroLentoTimeOut(){
        var carroDevagar = (Carros)_novoCarro.Instance();
        AddChild(carroDevagar);
        carroDevagar.Position = new Vector2(
            x: - 10,
            y: _pdevagar[_rand.Next(0, _pdevagar.Length)]
        );
        carroDevagar.LinearVelocity = new Vector2(
            x: _rand.Next(300, 325),
            y: 0
        );
    }

    public void OnPlayerPontua(){
        if(_score1 < 10){
            _score1 += 1;
            GetNode<AudioStreamPlayer>("Ponto").Play();
            GetNode<Label>("Placar1").Text = _score1.ToString();
        }
        if(_score1 >= 10){
            GetNode<Button>("Button").Show();
            GetNode<Label>("Resultado").Text = "P1 Ganhou!";
            GetNode<AudioStreamPlayer>("Tema").Stop();
            GetNode<AudioStreamPlayer>("Ganhou").Play();
            GetNode<Timer>("TimerCarroRapido").Stop();
            GetNode<Timer>("TimerCarroLento").Stop();

        }
    }
    public void OnPlayer2Pontua(){
        if(_score2 < 10){
            _score2 += 1;
            GetNode<AudioStreamPlayer>("Ponto").Play();
            GetNode<Label>("Placar2").Text = _score2.ToString();
        }
        if(_score2 >= 10){
            GetNode<Button>("Button").Show();
            GetNode<Label>("Resultado").Text = "P2 Ganhou!";
            GetNode<AudioStreamPlayer>("Tema").Stop();
            GetNode<AudioStreamPlayer>("Ganhou").Play();
            GetNode<Timer>("TimerCarroRapido").Stop();
            GetNode<Timer>("TimerCarroLento").Stop();

        }
    }

    public void OnButtonPressed(){
        _score1 = 0;
        _score2 = 0;
        GetNode<Button>("Button").Hide();
        GetNode<Label>("Placar1").Text = "0";
        GetNode<Label>("Placar2").Text = "0";
        GetNode<Label>("Resultado").Text = "";
        GetNode<Timer>("TimerCarroRapido").Start();
        GetNode<Timer>("TimerCarroLento").Start();
        GetNode<Player>("Player").VoltaPos();
        GetNode<Player2>("Player2").VoltaPos();
        GetNode<AudioStreamPlayer>("Tema").Play();
    }
}
