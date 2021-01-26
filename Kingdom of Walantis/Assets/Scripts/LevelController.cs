using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField]
    private GameObject
        inventoryObject;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(int index)
    {
        StartCoroutine(waitress());
        SceneManager.LoadScene(index);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.name == "Player")
        {
            
            int newIndex = (SceneManager.GetActiveScene().buildIndex) + 1;
            DontDestroyOnLoad(collisionObject);
            DontDestroyOnLoad(GameObject.Find("Interactables"));
            NextLevel(newIndex);
            collisionObject.transform.position = Vector2.zero;
        }

    }
    IEnumerator waitress()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Just wait 5");
    }

}