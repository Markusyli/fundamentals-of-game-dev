using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRadius = 3f;
    public Transform interactionTransform;

    private bool hasFocus;
    private bool hasInteracted;
    private Transform player;

    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }

    void Update()
    {
        
    }

    public void OnFocus(Transform playerTransform)
    {
        hasFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
    }
}
