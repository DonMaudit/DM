using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReloadScene : MonoBehaviour
{
    private int currentSceneindex;
    [SerializeField] private bool loadCurrentScene;
    [SerializeField] private int sceneIndex;
    [SerializeField] private float delay;

    [Header("Visuel")]
    [SerializeField] private bool playFX;
    private ParticleSystem fxToPlay;
    [SerializeField] private bool showText;
    [SerializeField] private TMP_Text uiText;
    [SerializeField] private string text;
    
    private void Start()
    {
        
        if (loadCurrentScene)
        {
            currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ça part");
        if (other.CompareTag("Player"))
        {
            if (playFX)
            {
                fxToPlay = other.gameObject.GetComponentInChildren<ParticleSystem>();
                fxToPlay.Play();
            }

            if (showText)
            {
                uiText.text = text;
                uiText.enabled = true;
            }
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        Debug.Log("oui");
        float timer;
        timer = 0f;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (loadCurrentScene)
        {
            SceneManager.LoadScene(currentSceneindex);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
        yield return null;
    }
}
