using EventArgs;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public Interaction interaction;
    public GameObject itemHolder;
    public Item item;
    public ChestOpenEvent onOpen = new();

    private Animator thisAnimator;


    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        interaction.OnInteraction += OnInteraction;
        interaction.SetActionText("Open Chest");
    }

    private void OnInteraction(object sender, InteractionEventArgs args)
    {
        if(item == null)
            Debug.Log("Jogador acabou de interagir com o baú, mas está vazio!");
        else
            Debug.Log("Jogador acabou de interagir com o baú, contendo item " + item.displayName + "!");

        interaction.SetAvailable(false);
        thisAnimator.SetTrigger("tOpen");

        //Create item object
        var itemObjectPrefab = item.objectPrefab;
        var position = itemHolder.transform.position;
        var rotation = itemObjectPrefab.transform.rotation;
        var itemObject = Instantiate(itemObjectPrefab, position, rotation);
        itemObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        itemObject.transform.SetParent(itemHolder.transform);

        //Update Inventory
        var itemType = item.itemType;
        if (itemType == ItemType.KEY)
            GameManager.Instance.keys++;
        else if(itemType == ItemType.BOSSKEY)
            GameManager.Instance.hasBoosKey = true;
        else if(itemType == ItemType.POTION)
        {
            var player = GameManager.Instance.player;
            var playerLife = player.GetComponent<LifeScript>();
            playerLife.Heal();
        }

        onOpen?.Invoke(gameObject);
    }
}

[Serializable] public class ChestOpenEvent : UnityEvent<GameObject> { }
