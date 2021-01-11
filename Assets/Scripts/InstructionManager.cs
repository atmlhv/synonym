using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    [SerializeField]
    GameObject instructiontext = default;

    const float textscale = 1.1f;
    const float textduration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Sequence titletextsequence = DOTween.Sequence()

        .OnStart(() =>
        {
            instructiontext.GetComponent<CanvasGroup>().alpha = 1.0f;
            instructiontext.transform.localScale = Vector3.one * textscale;
        })

        .Append(instructiontext.transform
        .DOScale(1.0f, textduration)
        .SetEase(Ease.InCubic))

        .Join(instructiontext.GetComponent<CanvasGroup>()
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
