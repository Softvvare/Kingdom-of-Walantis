using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public bool isInventoryOpen = false;
    [Header("UI Items")]
    public GameObject uiWindow;
    public Image[] itemsImages;
    [Header("UI Item Description")]
    public GameObject uiDescriptionWindow;
    public Image descriptionImage;
    public Text descriptionText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        uiWindow.SetActive(isInventoryOpen);
    }

    public void PickUp(GameObject item)
    {
        items.Add(item);
        UpdateUI();
    }

    private void UpdateUI()
    {
        HideItems();
        for (int i = 0; i < items.Count; i++)
        {
            //Debug.Log(items[i]);
            itemsImages[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            itemsImages[i].gameObject.SetActive(true);
        }
    }

    private void HideItems()
    {
        foreach (var item in itemsImages)
        {
            item.gameObject.SetActive(false);
        }
        HideDescription();
    }

    public void ShowDescription(int id)
    {
        descriptionImage.sprite = itemsImages[id].sprite;
        descriptionText.text = items[id].name + "\n\n" + items[id].GetComponent<Interactable>().descriptionText; ;
        descriptionImage.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        descriptionImage.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }

    public void Consume(int id)
    {
        if (items[id].GetComponent<Interactable>().itemType == Interactable.ItemType.Consumable)
        {
            Debug.Log("Consumed " + items[id].name);
            ///
            PowerUp(items[id].name);
            ///
            items[id].GetComponent<Interactable>().consumeEvent.Invoke();
            Destroy(items[id], 0.1f);
            //items.Remove(items[id]);
            items.RemoveAt(id);
            UpdateUI();
        }
    }

    public void PowerUp(string item)
    {
        Debug.Log("ITEM NAME" + item);
        if (item == "Potion of Power")
        {
            
            PlayerPrefs.SetInt("lightAttackDamage", 30);
            PlayerPrefs.SetInt("attackDamage", 40);

        }
    }

}
