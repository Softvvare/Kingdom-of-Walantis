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
            
            int index = (SceneManager.GetActiveScene().buildIndex);
            DontDestroyOnLoad(collisionObject);
            //DontDestroyOnLoad(inventoryObject.gameObject);
            DontDestroyOnLoad(GameObject.Find("Interactables"));
            NextLevel(index + 1);
            collisionObject.transform.position = Vector2.zero;

            /*
            try
            {
                Scene loadingScene = SceneManager.GetSceneByBuildIndex(index);
                SceneManager.LoadScene(loadingScene.name, LoadSceneMode.Additive);
                SceneManager.MoveGameObjectToScene(collisionObject, loadingScene);

            }
            catch (System.Exception)
            {
                Debug.LogError("Error occured on loading scene!");
                throw;
            }
            */
        }

    }
    IEnumerator waitress()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Just wait 5");
    }

}