using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    private RectTransform fader;
    private Image img;

    private void Start()
    {
        fader = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    public delegate void OnCompleteDelegate();
    
    public void FadeIn(float time, OnCompleteDelegate method = null)
    {
        img.raycastTarget = true;
        LeanTween.alpha(fader, 1f, time)
            .setFrom(0f)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                img.raycastTarget = false;
                method?.Invoke();   
            });
    }
    
    public void FadeOut(float time, OnCompleteDelegate method = null)
    {
        img.raycastTarget = true;
        LeanTween.alpha(fader, 0f, time)
            .setFrom(1f)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                img.raycastTarget = false;
                method?.Invoke();   
            });
    }
}
