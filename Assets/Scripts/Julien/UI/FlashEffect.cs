using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;
    
    private Image _image;
    private Material _originalMaterial;
    private Coroutine _flashRoutine;

    public void StartFlashEffect(GameObject go, Color color)
    {
        _image = go.GetComponent<Image>();
        _originalMaterial = _image.material;
        flashMaterial = new Material(flashMaterial);
        Flash(color);
    }
    

    private void Flash(Color color)
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }
        _flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color)
    {
        _image.material = flashMaterial;
        flashMaterial.color = color;
        yield return new WaitForSeconds(duration);
        _image.material = _originalMaterial;
        _flashRoutine = null;
    }
    
}

