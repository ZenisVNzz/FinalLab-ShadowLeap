using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsContainer;
    private GameObject[] hearts;

    public void Initialize(int maxHealth)
    {
        if (hearts != null)
        {
            foreach (var heart in hearts)
            {
                Destroy(heart);
            }
        }

        hearts = new GameObject[maxHealth];
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartsContainer);
            hearts[i].AddComponent<HeartState>();
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            var heartState = hearts[i].GetComponent<HeartState>();
            if (heartState.isActive == true && i >= currentHealth)
            {
                heartState.isActive = false;
                hearts[i].GetComponent<Animator>().Play("LostHealth");
            }
        }
    }
}
