using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    GameObject titletext = default;

    const float textscale = 1.1f;
    const float textduration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Sequence titletextsequence = DOTween.Sequence()

        .OnStart(() =>
        {
            titletext.GetComponent<CanvasGroup>().alpha = 1.0f;
            titletext.transform.localScale = Vector3.one * textscale;
        })

        .Append(titletext.transform
        .DOScale(1.0f, textduration)
        .SetEase(Ease.InCubic))

        .Join(titletext.GetComponent<CanvasGroup>()
        .DOFade(0.0f, textduration)
        .SetEase(Ease.InCubic))

        .SetLoops(-1, LoopType.Yoyo)

        .Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
