using Godot;
using System;
using System.Linq;

public class Main : Node
{
    private Random _rand = new Random();
    private PackedScene _novoCarro;
    private int[] _pdevagar = {600, 544, 324, 384, 216, 160};
    private int[] _prapido = {488, 272, 104};
    public override void _Ready()
    {
        _novoCarro = ResourceLoader.Load<PackedScene>("res://Scenes/Carros.tscn");
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
    public void OnPlayerPontua(Player player){
        MarcaPonto(player);
        UpatePlacares(player);
        VerificaGanhador(player);
    }
    private void VerificaGanhador(Player player)
    {
        var playerName = player.PlayerName.ToString().Replace("_"," ");
        if(player.Score >= 10){
            GetNode<Button>("Button").Show();
            GetNode<Label>("Resultado").Text = $"{playerName} Ganhou!";
            GetNode<AudioStreamPlayer>("Tema").Stop();
            GetNode<AudioStreamPlayer>("Ganhou").Play();
            GetNode<Timer>("TimerCarroRapido").Stop();
            GetNode<Timer>("TimerCarroLento").Stop();
        }
    }
    private void MarcaPonto(Player player){
        if(player.Score < 10){
            GD.Print(player.PlayerName);
            player.Pontuar();
            GetNode<AudioStreamPlayer>("Ponto").Play();
        }
    }
    private void UpatePlacares(Player player){
        if(player.PlayerName == PlayerName.PLAYER_1){
            GetNode<Label>("Placar1").Text = player.Score.ToString();
        }else if(player.PlayerName == PlayerName.PLAYER_2){
            GetNode<Label>("Placar2").Text = player.Score.ToString();
        }
    }
    public void OnButtonPressed(){
        ResetPlayers();
        ResetUI();
    }

    private void ResetPlayers(){
        var players = GetTree().GetNodesInGroup("Player").Cast<Player>();
        foreach (var player in players){
            player.ResetScore();
            player.ResetPosition();
        }
    }
    private void ResetUI(){
        GetNode<Button>("Button").Hide();
        GetNode<Label>("Placar1").Text = "0";
        GetNode<Label>("Placar2").Text = "0";
        GetNode<Label>("Resultado").Text = "";
        GetNode<Timer>("TimerCarroRapido").Start();
        GetNode<Timer>("TimerCarroLento").Start();
        GetNode<AudioStreamPlayer>("Tema").Play();
    }
}
