using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Detection")]
    public Transform detectP;
    private const float detectRadius = 0.8f;
    public LayerMask detectLayer;
    public GameObject detectedObject; // store detected object in gameobject
    [Header("Examine")]
    public GameObject examineWindow;
    public Image examineImage;
    public GameObject examineShow;
    public Text examineText;
    public bool isExamining;

    void Update()
    {
        if (DetectItems())
        {
            detectedObject.GetComponent<Interactable>().Interact(examineShow);
        }
        else
            examineShow.SetActive(false);
    }

    private bool DetectItems()
    {
        Collider2D obje = Physics2D.OverlapCircle(detectP.position, detectRadius, detectLayer);

        if (obje == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obje.gameObject;
            return true;
        }
    }

    public void Examine(Interactable interactable)
    {
        if (isExamining)
        {
            examineWindow.SetActive(false);
            isExamining = false;
        }
        else
        {
            examineImage.sprite = interactable.GetComponent<SpriteRenderer>().sprite;
            examineText.text = interactable.descriptionText;
            examineWindow.SetActive(true);
            isExamining = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectP.position, detectRadius);
    }
}
