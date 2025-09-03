using System.Collections;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnInterval = 0.1f;

    private SpriteRenderer spriteRenderer;
    private float timer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Init()
    {
        while (true)
        {
            GameObject gameObject = Instantiate(prefab, transform.position, transform.rotation);
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            Animator animator = gameObject.GetComponent<Animator>();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            sr.sprite = spriteRenderer.sprite;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StartTrail()
    {
        StartCoroutine(Init());
    }

    public void StopTrail()
    {
        StopAllCoroutines();
    }
}
