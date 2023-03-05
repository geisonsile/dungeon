using EventArgs;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Interaction interaction;
    public Item requiredKey;
    private bool isOpen;
    private Animator thisAnimator;
    

    private void Awake()
    {
        thisAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        interaction.OnInteraction += OnInteraction;
        interaction.SetActionText("Open Door");
    }

    private void Update()
    {
        if (!isOpen)
        {
            var hasKey = false;
            if (requiredKey == null)
                hasKey = true;
            else if (requiredKey.itemType == ItemType.KEY)
                hasKey = GameManager.Instance.keys > 0;
            else if (requiredKey.itemType == ItemType.BOSSKEY)
                hasKey = GameManager.Instance.hasBoosKey;

            interaction.SetAvailable(hasKey);
        }
    }

    private void OnInteraction(object sender, InteractionEventArgs args)
    {
        Debug.Log("Jogador acabou de interagir com a porta!");

        if (!isOpen)
            OpenDoor();
        else
            CloseDoor();
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (requiredKey != null)
        {
            if (requiredKey.itemType == ItemType.KEY)
                GameManager.Instance.keys--;
            else if (requiredKey.itemType == ItemType.BOSSKEY)
                GameManager.Instance.hasBoosKey = false;
        }
        
        interaction.SetAvailable(false);
        
        thisAnimator.SetTrigger("tOpen"); 
    }
    private void CloseDoor()
    {
        isOpen = false;

        //interaction.SetAvailable(false);
        thisAnimator.SetTrigger("tClose");
    }
}
