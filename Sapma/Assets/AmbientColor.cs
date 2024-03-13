using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientColor : MonoBehaviour
{

    [System.Serializable] public class Preset
    {
        public bool fog;
        public Color fogcolor;
        public Color ambcolor;
        public float fogdens;
    }


    public Preset[] ColorSets;

    public void Awake()
    {
  


    }

    [ContextMenu("ChooseSet")]
    public void Randomize()
    {
        ChooseSet(Random.Range(0, ColorSets.Length));

    }
    public void ChooseSet(int Set)
    {
        RenderSettings.fog = ColorSets[Set].fog;
        RenderSettings.ambientLight = ColorSets[Set].ambcolor;
        RenderSettings.fogColor = ColorSets[Set].fogcolor;
        RenderSettings.fogDensity = ColorSets[Set].fogdens;
    }

}
