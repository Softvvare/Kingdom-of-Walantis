using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour // inherit from PhysicsObject + Rigidbody(kinematic) !?
{
    public enum InteractionType { NONE, PickUp, Examine };
    public enum ItemType { Static, Consumable };
    [Header("Features")]
    public InteractionType interactionType;
    public ItemType itemType;
    public string descriptionText;
    [Header("Events")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;

    private void Reset()
    {
        // use this method to assign default values
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10; // 10: Interactable
    }

    public void Interact()
    {
        if(interactionType == InteractionType.PickUp)
        {
            FindObjectOfType<InventoryController>().PickUp(gameObject);
            gameObject.SetActive(false);
        }
        else if(interactionType == InteractionType.Examine)
        {
            if(Input.GetKeyDown(KeyCode.E))
                FindObjectOfType<InteractionController>().Examine(this);
        }
        else
        { 
            //Debug.Log("None");
        }
    }
}
