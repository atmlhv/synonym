using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ultManager : MonoBehaviour
{
    //仮
    [SerializeField]
    Text text = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = alpacaManager.ultpoint.ToString() + " / " + alpacaManager.ultthreshold.ToString();
    }
}
