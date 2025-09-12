using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Animator transitionAnim;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        StartCoroutine(Play());
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator Play()
    {
        transitionAnim.Play("Transition");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
    }
}
