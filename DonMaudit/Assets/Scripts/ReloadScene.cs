using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    private int currentSceneindex;
    [SerializeField] private bool loadCurrentScene;
    [SerializeField] private int sceneIndex;
    private void Start()
    {
        if (loadCurrentScene)
        {
            currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (loadCurrentScene)
            {
                SceneManager.LoadScene(currentSceneindex);
            }
            else
            {
                SceneManager.LoadScene(sceneIndex);
            }
            
        }
    }
}
