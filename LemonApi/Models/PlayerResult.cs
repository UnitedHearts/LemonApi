﻿namespace LemonApi.Models;

public class PlayerResult
{
    public string Email { get; set; }
    public int Rank { get; set; }
    public int Coins { get; set; }
    public int Fails { get; set; }
    public int Punches { get; set; }
    public double SpawnTimePoint { get; set; }
    public double? DeadTimePoint { get; set; }
}
