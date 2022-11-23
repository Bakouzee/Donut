using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    [SerializeField] private float fadeDuration;
    private bool _isFaded = false;

    [ContextMenu("StartFade")]
    public void StartFade()
    {
        Fader();
    }
    public void Fader()
    {
        _isFaded = !_isFaded;

        if (_isFaded)
        {
            imageToFade.DOFade(1, fadeDuration);
        }
        else
        {
            imageToFade.DOFade(0, fadeDuration);
        }

        if(_isFaded)
            StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(fadeDuration);
        Fader();
    }

}
