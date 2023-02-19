using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isFading = false;
    [SerializeField] private bool _isFadeTransparent = false;
    [SerializeField] private Image _fadeImageReference;


    //Monobehaviors
    private void Awake()
    {
        if (_fadeImageReference == null)
            _fadeImageReference = GetComponent<Image>();

        if (_fadeImageReference != null)
            UpdateFadeTransparencyStatus();

        else Debug.LogError("ScreenFadeController ERROR: No fade Image can be found on object " + gameObject);
    }

    private void Start()
    {
        FadeToTransparent(1);
    }


    //Utilites
    private void UpdateFadeTransparencyStatus()
    {
        _isFadeTransparent = _fadeImageReference.canvasRenderer.GetAlpha() <= 0.05f;
    }

    private void UpdateFadeStatusAfterFadeEffect()
    {
        UpdateFadeTransparencyStatus();
        _isFading = false;
    }

    public void FadeToBlack(float duration)
    {
        if (_isFadeTransparent && !_isFading)
        {
            _isFading = true;
            _fadeImageReference.CrossFadeAlpha(1, duration, false);
            Invoke("UpdateFadeStatusAfterFadeEffect", duration);
        }
    }

    public void FadeToTransparent(float duration)
    {
        if (!_isFadeTransparent && !_isFading)
        {
            _isFading = true;
            _fadeImageReference.CrossFadeAlpha(0, duration, false);
            Invoke("UpdateFadeStatusAfterFadeEffect", duration);
        }
    }

    public bool IsFadeInProgress()
    {
        return _isFading;
    }

    public bool IsFadeTransparent()
    {
        return _isFadeTransparent;
    }

    //private void TestFadeOnInput()
    //{
    //    //fadeOut
    //    if (Input.GetKeyDown(KeyCode.Q))
    //        FadeToBlack(2);

    //    //fadeIn
    //    if (Input.GetKeyDown(KeyCode.E))
    //        FadeToTransparent(2);
    //}

}
