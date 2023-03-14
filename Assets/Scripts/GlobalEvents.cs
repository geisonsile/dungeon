using EventArgs;
using System;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents Instance { get; private set; }

    public event EventHandler<BossDoorOpenArgs> OnBossDoorOpen;
    public event EventHandler<BossRoomEnterArgs> OnBossRoomEnter;
    public event EventHandler<GameOverArgs> OnGameOver;
    public event EventHandler<GameWonArgs> OnGameWon;


    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void InvokeBossDoorOpen(object sender, BossDoorOpenArgs args) { OnBossDoorOpen?.Invoke(sender, args);}  
    public void InvokeBossRoomEnter(object sender, BossRoomEnterArgs args) { OnBossRoomEnter?.Invoke(sender, args);}
    public void InvokeGameOver(object sender, GameOverArgs args) { OnGameOver?.Invoke(sender, args);}
    public void InvokeGameWon(object sender, GameWonArgs args) { OnGameWon?.Invoke(sender, args);}
}
