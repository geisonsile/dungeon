using UnityEngine;

public class AttachTo : MonoBehaviour
{
    public GameObject objectToFollow;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(objectToFollow != null)
        {
            transform.position = objectToFollow.transform.position;
        }
    }
}
