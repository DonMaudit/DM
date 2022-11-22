using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    [SerializeField] private Image mapSprite;
    private Color mapColor;
    [SerializeField] private AnimationCurve openMap;
    [SerializeField] private AnimationCurve closeMap;
    [SerializeField] private float curveDuration;

    private float timer;
    private float alphaValue;

    private bool opened;
    private bool ready = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        mapColor = mapSprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ready);
        if (Input.GetButtonDown("map"))
        {
            
            if (opened)
            {
                if (ready)
                {
                    Debug.Log("nope");
                    opened = false;
                    StartCoroutine("AlphaValue", curveDuration);
                }
            }
            else
            {
                if (ready)
                {
                    Debug.Log("ouaip");
                    opened = true;
                    StartCoroutine("AlphaValue", curveDuration); 
                }
            }
        }
    }

    

    private IEnumerator AlphaValue(float newTime)
    {
        ready = false;
        timer = 0f;
        AnimationCurve currentCurve;
        if (!opened)
        {
            currentCurve = closeMap;
        }
        else
        {
            currentCurve = openMap;
        }

        while (timer < 1)
        {
            
            alphaValue = currentCurve.Evaluate(timer);
            
            mapColor.a = alphaValue;
            mapSprite.color = mapColor;
                
            timer += Time.deltaTime / curveDuration;
            
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        StartCoroutine("cooldown");
    }

    private IEnumerator cooldown()
    {
        float newTimer;
        newTimer = 0f;
        while (newTimer < 5)
        {
            newTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        ready = true;
    }
}
