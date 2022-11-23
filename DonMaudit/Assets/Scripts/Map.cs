using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{

    [SerializeField] private Image mapSprite;
    private Color mapColor;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float curveDuration;

    private float timer;
    private float alphaValue;

    private bool ready = true;

    [SerializeField] private TMP_Text UIUtilisations;
    [SerializeField] private int nbUses;

    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private float cooldownTime;
    private float cdValue;
    
    

    // Start is called before the first frame update
    void Start()
    {
        mapColor = mapSprite.color;
        UIUtilisations.text = nbUses.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ready);
        if (Input.GetButtonDown("map"))
        {
            
            if (ready && nbUses> 0)
            {
                StartCoroutine("AlphaValue", curveDuration);
                nbUses -= 1;
                UIUtilisations.text = nbUses.ToString();
            }
            
        }
    }

    

    private IEnumerator AlphaValue(float newTime)
    {
        ready = false;
        timer = 0f;

        while (timer < 1)
        {
            
            alphaValue = curve.Evaluate(timer);
            
            mapColor.a = alphaValue;
            mapSprite.color = mapColor;
                
            timer += Time.deltaTime / curveDuration;
            
            yield return new WaitForEndOfFrame();
        }

        
        StartCoroutine(CoolDown());
        yield return null;
        
    }

    private IEnumerator CoolDown()
    {
        cooldownText.enabled = true;
        float newTimer;
        newTimer = 0f;
        while (newTimer < cooldownTime)
        {
            cdValue = cooldownTime - newTimer.RoundF(0);
            newTimer += Time.deltaTime;
            cooldownText.text = cdValue.ToString();
            yield return new WaitForEndOfFrame();
        }
        cooldownText.enabled = false;
        ready = true;
    }

    
}
