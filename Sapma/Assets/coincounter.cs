using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coincounter : MonoBehaviour
{


    public int coin;
    public Text text;

   [SerializeField] static public coincounter instance;
    private void Awake()
    {
        instance = this;

    }

    public void CoinGot()
    {
        coin++;
        text.text = coin.ToString();

    }
}
