using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class dialoguer : MonoBehaviour
{
    [ExecuteInEditMode]
    float texttimer;
    TextMeshPro text;
    LookAtConstraint cons;
    public string[] phrases;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        cons = GetComponent<LookAtConstraint>();

        ConstraintSource i = new ConstraintSource();
        i.sourceTransform = Camera.main.transform;
        i.weight = 1f ;
        cons.AddSource(i);
    }

    private void FixedUpdate()
    {
       if(texttimer > 0) texttimer -= Time.deltaTime;
    }

    public void Speak(float time,int phrase)
    {
       StartCoroutine(talk(time,phrases[phrase]));
       texttimer = time;
    }
    IEnumerator talk(float time,string words)
    {
        text.text = words;

        while(texttimer  >= 0)
        {            
        yield return null; 
        }

        text.text = null;

    }
}