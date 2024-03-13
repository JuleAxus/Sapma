using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cam.Effects;

public class RandomizeStuff : MonoBehaviour
{
    AmbientColor ambientcolor;
    Posterize posterize;
    RetroSize retrosize;
    public AudioSource musicsr;
    public AudioClip[] clips;

    public GameObject[] scripts;


    [ContextMenu("Randomize All")]


    public void Awake()
    {
        RandomizeAll();
        musicsr.clip = clips[Random.Range(0, clips.Length)];
        musicsr.Play();
    

    }
    public void RandomizeAll()
    {
        foreach(GameObject i in scripts)
        {
            i.SendMessage("Randomize",SendMessageOptions.DontRequireReceiver);
            Debug.Log(i.name);
        }

        

    }



}
