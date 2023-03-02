using EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Interaction interaction;
    
    void Start()
    {
        interaction.OnInteraction += OnInteraction;
    }
	
    void Update()
    {
        
    }

    private void OnInteraction(object sender, InteractionEventArgs args)
    {
        Debug.Log("Jogador acabou de interagir com o baú!");
        interaction.SetAvailable(false);
    }
}
