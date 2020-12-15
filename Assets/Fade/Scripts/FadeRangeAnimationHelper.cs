//https://teratail.com/questions/242941

using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(IFade))]
public class FadeRangeAnimationHelper : MonoBehaviour
{
    [Range(0, 1)] public float range;
    private IFade fade;

    private void Update()
    {
        if (this.fade == null)
        {
            this.fade = this.GetComponent<IFade>();
        }

        if (this.fade.Range != this.range)
        {
            this.fade.Range = this.range;
        }
    }
}