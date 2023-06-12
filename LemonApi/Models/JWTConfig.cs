﻿namespace LemonApi.Models;

public class JWTConfig
{
    public static readonly JWTConfig Default = new JWTConfig()
    {
        Audience = "LemonSky",
        Issuer = "UnitedHearts",
        UseLifeTime = false,
    };
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public bool UseLifeTime { get; set; }
    public double LifeTime { get; set; }
}
