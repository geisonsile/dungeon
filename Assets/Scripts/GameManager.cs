using BossBattle;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance {get; private set;}

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
    }

    void Update()
    {
        bossBattleHandler.Update();    
    }
}
