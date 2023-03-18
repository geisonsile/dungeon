using BossBattle;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance {get; private set;}

    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool isGameWon;

    public GameObject player;
    public List<Interaction> interactionList;
    
    [Header("Rendering")]
    public Camera worldUiCamera;
    
    [Header("Physics")]
    [SerializeField] public LayerMask groundLayer;

    [Header("Inventory")]
    public int keys;
    public bool hasBoosKey;

    [Header("Boss")]
    public GameObject boss;
    public GameObject bossBattleParts;
    public BossBattleHandler bossBattleHandler;
    public GameObject bossDeathSequence;

    // Music
    [Header("Music")]
    public AudioSource gameplayMusic;
    public AudioSource bossMusic;
    public AudioSource ambienceMusic;

    [Header("UI")]
    public GameplayUI gameplayUI;


    void Awake() 
    {
        // Singleton
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this; 
    }

    void Start()
    {
        bossBattleHandler = new BossBattleHandler();

        // Play music
        var musicTargetVolume = gameplayMusic.volume;
        gameplayMusic.volume = 0;
        gameplayMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, musicTargetVolume, 1f));

        // Play ambience
        var ambienceTargetVolume = ambienceMusic.volume;
        ambienceMusic.volume = 0;
        ambienceMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(ambienceMusic, ambienceTargetVolume, 1f));

        // Listen to OnGameOver
        GlobalEvents.Instance.OnGameOver += (sender, args) => isGameOver = true;
        GlobalEvents.Instance.OnGameWon += (sender, args) => isGameWon = true;
    }

    void Update()
    {
        bossBattleHandler.Update();    
    }
}
