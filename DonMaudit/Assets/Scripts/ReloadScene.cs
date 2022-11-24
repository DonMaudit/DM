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
    [SerializeField] private GameObject ui;
    private TMP_Text uiText;
    [SerializeField] private string text;

    [Header("Anims")] 
    [SerializeField] private Animator m_animator;
    [SerializeField] private bool playDeathAnim;
    [SerializeField] private bool playCollectAnim;
    
    
    
    private void Start()
    {
        
        if (loadCurrentScene)
        {
            currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        }

        uiText = ui.GetComponent<TMP_Text>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playFX)
            {
                fxToPlay = other.gameObject.GetComponentInChildren<ParticleSystem>();
                fxToPlay.Play();
            }

            if (showText)
            {
                ui.gameObject.SetActive(true); 
                uiText.text = text;
            }

            if (playDeathAnim)
            {
                m_animator.SetBool("isDead", true);
            }

            if (playCollectAnim)
            {
                m_animator.SetTrigger("collect");
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
