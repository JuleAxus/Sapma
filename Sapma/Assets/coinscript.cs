using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinscript : MonoBehaviour
{

    public coincounter counter;
   public  AudioSource ausour;
    public Color[] colors;
    private void Start()
    {
        Randomize();
        counter = coincounter.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        ausour.Play();
        counter.CoinGot();
        Destroy(this.gameObject);
    }


    public void Randomize()
    {
        float i = Random.Range(0f,1f);


        GetComponent<Renderer>().sharedMaterial.SetColor("_Color0", colors[Random.Range(0, colors.Length)]);

        if (i < 0.5f) this.gameObject.SetActive(false);
       
        
    }


}
