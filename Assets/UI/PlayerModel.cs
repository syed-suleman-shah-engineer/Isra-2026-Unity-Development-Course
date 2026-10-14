using System;
using UnityEngine;

public class PlayerModel
{
    public event Action<PlayerModel> OnPlayerDataChanged;

    public string Name { get; private set; }
    public float Health { get; private set; }
    public byte[] Avatar { get; private set; }
    public int Kills { get; private set; }
    public int Deaths { get; private set; }

    public void SetName(string name)
    {
        Name = name;
        OnPlayerDataChanged?.Invoke(this);
    }

    public void SetHealth(float health)
    {
        Health = Mathf.Clamp(health, 0f, 100f);
        OnPlayerDataChanged?.Invoke(this);

    }

    public void SetAvatar(byte[] avatar)
    {
        Avatar = avatar;
        OnPlayerDataChanged?.Invoke(this);
    }

    public void SetKills(int kills)
    {
        Kills = Mathf.Max(0, kills);
        OnPlayerDataChanged?.Invoke(this);
    }

    public void SetDeaths(int deaths)
    {
        Deaths = Mathf.Max(0, deaths);
        OnPlayerDataChanged?.Invoke(this);
    }

    public void AddKill()
    {
        Kills++;
        OnPlayerDataChanged?.Invoke(this);
    }

    public void AddDeath()
    {
        Deaths++;
        OnPlayerDataChanged?.Invoke(this);
    }
}