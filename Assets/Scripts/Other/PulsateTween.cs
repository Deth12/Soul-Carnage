using UnityEngine;

public class PulsateTween : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float pulsateRate = 0.5f;
    [SerializeField] private float scaleMultiplier = 2f;
    
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        LeanTween
            .scale(rect, rect.localScale * scaleMultiplier, pulsateRate)
            .setLoopPingPong()
            .setIgnoreTimeScale(true);
    }
}
