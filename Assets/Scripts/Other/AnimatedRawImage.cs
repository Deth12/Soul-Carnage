using UnityEngine;
using UnityEngine.UI;

public class AnimatedRawImage : MonoBehaviour
{
    [SerializeField] float animSpeed = 0.25f;

    private RawImage img;

    private void Start()
    {
        img = GetComponent<RawImage>();
    }

    private void Update()
    {
        Rect uvRect = img.uvRect;
        uvRect.x -= animSpeed * Time.deltaTime;
        img.uvRect = uvRect;
    }
}
