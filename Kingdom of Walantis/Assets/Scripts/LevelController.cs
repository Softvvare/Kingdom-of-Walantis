using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.transform.position = Vector2.zero;
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
            NextLevel(index + 1);
            collisionObject.transform.position = Vector2.zero;

        }

    }
    IEnumerator waitress()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Just wait 5");
    }

}