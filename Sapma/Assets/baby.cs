using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baby : MonoBehaviour
{
    //he baby
    public dialoguer dialog;
   public void Shot()
    {
        Application.Quit();
    }

    [ContextMenu("Pet")]
    public void Pet()
    {
        dialog.Speak(6f,Random.Range(0,dialog.phrases.Length));
    }
}
