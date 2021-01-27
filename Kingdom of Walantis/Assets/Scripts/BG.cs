using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BG : MonoBehaviour
{
    [Header("Backgrounds")]
    public Transform[] backgrounds;
    [Header("Attributes")]
    public float[] parallaxScales;
    public float smoothing;
    [Header("Cams")]
    private Transform camera;
    private Vector3 prevCamera;

    void Start()
    {
        InitBG();
        camera = Camera.main.transform;
        prevCamera = camera.position;
        parallaxScales = new float[backgrounds.Length];

        if(backgrounds.Length == 2)
            for (int i = 0; i < backgrounds.Length; i++)
                parallaxScales[i] = backgrounds[i].position.z * -1;
    }

    void Update()
    {
        int sceneIndex = (SceneManager.GetActiveScene().buildIndex);

        if (sceneIndex == 1)
            InitBG();
        else backgrounds = new Transform[1];
        
        if (backgrounds.Length == 2)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (prevCamera.x - camera.position.x) * parallaxScales[i];
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }
            prevCamera = camera.position;
        }
    }

    public void InitBG()
    {
        try
        {
            backgrounds = new Transform[2];
            backgrounds[0] = GameObject.Find("BG2").transform;
            backgrounds[1] = GameObject.Find("BG3").transform;
        }
        catch (UnityException e)
        {
            //Debug.Log(e);
        }
    }
}
