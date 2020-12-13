using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private Material _transitionIn;

    [SerializeField]
    private Material _transitionOut;

    [SerializeField]
    private UnityEvent OnTransition;
    [SerializeField]
    private UnityEvent OnComplete;

    void Start()
    {
        StartCoroutine(BeginTransition());
    }

    void Update()
    {
    }

    IEnumerator BeginTransition()
    {
        yield return Animate(_transitionIn, 1);
        if (OnTransition != null) { OnTransition.Invoke(); }
        yield return new WaitForEndOfFrame();

        yield return Animate(_transitionOut, 1);
        if (OnComplete != null) { OnComplete.Invoke(); }
    }

    /// <summary>
    /// time秒かけてトランジションを行う
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Animate(Material material, float time)
    {
        GetComponent<Image>().material = material;
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        material.SetFloat("_Alpha", 1);
    }
}
