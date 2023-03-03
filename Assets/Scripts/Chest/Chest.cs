using EventArgs;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Interaction interaction;
    public GameObject itemHolder;
    public Item item;

    private Animator thisAnimator;


    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        interaction.OnInteraction += OnInteraction;
    }
	
    void Update()
    {
        
    }

    private void OnInteraction(object sender, InteractionEventArgs args)
    {
        Debug.Log("Jogador acabou de interagir com o baú, contendo item " + item.displayName + "!");
        interaction.SetAvailable(false);
        thisAnimator.SetTrigger("tOpen");

        var itemObjectPrefab = item.objectPrefab;
        var position = itemHolder.transform.position;
        var rotation = itemObjectPrefab.transform.rotation;
        var itemObject = Instantiate(itemObjectPrefab, position, rotation);
        itemObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        itemObject.transform.SetParent(itemHolder.transform);
    }
}
