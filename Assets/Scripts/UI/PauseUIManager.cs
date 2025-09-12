using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToTitleButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private OnPlayerDeadHandler onPlayerDeadHandler;

    private InputSystem_Actions inputActions;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        backToTitleButton.onClick.AddListener(OnBackToTitleButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        pauseMenuUI.SetActive(false);

        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += ctx => OpenMenu();
    }

    private void OpenMenu()
    {
        if (pauseMenuUI.activeSelf)
        {
            OnContinueButtonClicked();
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;       
        }
    }

    private void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    private void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        onPlayerDeadHandler.Respawn();
    }

    private void OnBackToTitleButtonClicked()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
