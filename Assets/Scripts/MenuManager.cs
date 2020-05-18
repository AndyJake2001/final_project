using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public bool isPaused;
    private bool forceOpen = false;
    private string nextLevel;

    [SerializeField]
    private string mainMenu;

    [SerializeField]
    private GameObject menuUi;

    [SerializeField]
    private GameObject overlayUi;

    [SerializeField]
    private Button actionButton;

    private Text actionText;

    // Start is called before the first frame update
    void Start() {
        actionText = actionButton.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !forceOpen) {
            if (isPaused) {
                Resume();
            } else {
                this.actionText.text = "Resume";
                this.forceOpen = false;

                Pause();
            }
        }
    }

    void Resume() {
        overlayUi.SetActive(true);
        menuUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        overlayUi.SetActive(false);
        menuUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void triggerWin(string nextLevel) {
        if (nextLevel != "<END>") {
            this.actionText.text = "Next Level"; 
        } else {
            this.actionText.text = "SOON™";
        }
        forceOpen = true;

        Pause();

        this.nextLevel = nextLevel;
    }

    public void triggerLose() {
        this.actionText.text = "Restart Level";
        forceOpen = true;

        Pause();
    }

    public void OnActionButton() {
        var actionText = this.actionText.text;

        forceOpen = false;
        Resume();
        switch (actionText) {
            case "Resume":
                break;
            case "Next Level":
                SceneManager.LoadScene(nextLevel);
                break;
            case "Restart Level":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                break;
        }
    }

    public void OnMainMenuButton() {
        forceOpen = false;
        Resume();
        SceneManager.LoadScene(mainMenu);
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
