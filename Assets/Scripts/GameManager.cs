using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    void Awake() 
    {
        // Singleton
        if(Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Update() 
    {
        
    }
}
