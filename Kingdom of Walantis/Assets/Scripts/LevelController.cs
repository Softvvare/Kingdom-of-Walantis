using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.name == "Player")
        {
            int index = (SceneManager.GetActiveScene().buildIndex) + 1;

            DontDestroyOnLoad(collisionObject);
            SceneManager.LoadScene(index);
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
}