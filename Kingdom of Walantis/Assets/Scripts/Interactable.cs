using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour // inherit from PhysicsObject + Rigidbody(kinematic) !?
{
    public enum InteractionType { NONE, PickUp, Examine};
    public InteractionType type;
    // Examine
    public string descriptionText;

    private void Reset()
    {
        // use this method to assign default values
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10; // 10: Interactable
    }

    public void Interact()
    {
        if( type == InteractionType.PickUp)
        {
            FindObjectOfType<InventoryController>().PickUp(gameObject);
            gameObject.SetActive(false);
        }
        else if( type == InteractionType.Examine)
        {
            if(Input.GetKeyDown(KeyCode.E))
                FindObjectOfType<InteractionController>().Examine(this);
        }
        else
        { 
            Debug.Log("None");
        }
    }
}
