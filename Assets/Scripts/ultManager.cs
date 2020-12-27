using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ultManager : MonoBehaviour
{
    //仮
    [SerializeField]
    Text text = default;

    [SerializeField]
    Image CircleBar;

    [SerializeField]
    Image Bar;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip charged;

    bool IsCharged = false;

    // Start is called before the first frame update
    void Start()
    {
        CircleBar.color = new Color(0.5f, 0.5f, 0.2f, 1.0f);
        Bar.color = new Color(0.5f, 0.5f, 0.2f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = alpacaManager.ultpoint.ToString() + " / " + alpacaManager.ultthreshold.ToString();
        CircleBar.fillAmount = alpacaManager.UpdateUltpoint / 500;
        Bar.fillAmount = alpacaManager.UpdateUltpoint / 500;
        if(!IsCharged && alpacaManager.ultpoint == 500)
        {
            CircleBar.fillAmount = 1;
            Bar.fillAmount = 1;
            audioSource.PlayOneShot(charged);
            IsCharged = true;
            CircleBar.color = new Color(0.9f, 0.9f, 0.3f, 1.0f);
            Bar.color = new Color(0.9f, 0.9f, 0.3f, 1.0f);
        }
        if (alpacaManager.ultpoint==0)
        {
            Bar.fillAmount = 0;
            CircleBar.fillAmount = 0;
            IsCharged = false;
            CircleBar.color = new Color(0.5f, 0.5f, 0.2f, 1.0f);
            Bar.color = new Color(0.5f, 0.5f, 0.2f, 1.0f);
        }



    }
}
