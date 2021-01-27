using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public GameObject loadScreen;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.transform.position = Vector2.zero;
    }

    public void NextLevel(int index)
    {
        SceneManager.LoadScene(index);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.name == "Player")
        {
            int index = (SceneManager.GetActiveScene().buildIndex);
            collisionObject.GetComponent<PlayerController>().Load();
            DontDestroyOnLoad(collisionObject);           
            NextLevel(index + 1);
            collisionObject.transform.position = Vector2.zero;
        }

    }
}