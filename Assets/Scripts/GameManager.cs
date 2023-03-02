using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    // Singleton
    public static GameManager Instance {get; private set;}

    //Interaction
    public List<Interaction> interactionList;

    //Rendering
    [Header("Rendering")]
    public Camera worldUiCamera;

    //Physics
    [Header("Physics")]
    [SerializeField] public LayerMask groundLayer;
    public GameObject player;


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
