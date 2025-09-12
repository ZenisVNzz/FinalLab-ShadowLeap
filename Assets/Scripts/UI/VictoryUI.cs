using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] GameObject victoryUI;
    [SerializeField] Button backToTitle;

    public void Active()
    {
        victoryUI.SetActive(true);
        backToTitle.onClick.AddListener(BackToTitle);
    }

    private void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
