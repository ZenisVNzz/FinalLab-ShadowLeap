using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private float squashAmount = 0.3f;
    [SerializeField] private float stretchAmount = 0.2f; 
    [SerializeField] private float duration = 0.1f;   

    private Vector3 originalScale;
    private bool isTweening;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnJump()
    {
        if (!isTweening) StartCoroutine(DoSquashStretch(Vector3.up * stretchAmount));
    }

    public void OnLand()
    {
        if (!isTweening) StartCoroutine(DoSquashStretch(Vector3.down * squashAmount));
    }

    private System.Collections.IEnumerator DoSquashStretch(Vector3 dir)
    {
        isTweening = true;
        Vector3 targetScale = new Vector3(
            originalScale.x - dir.y,
            originalScale.y + dir.y,
            originalScale.z
        );

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
        isTweening = false;
    }
}