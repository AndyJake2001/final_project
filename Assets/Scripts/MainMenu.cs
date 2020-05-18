using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private GameObject creditsUi;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPlayButton() {
        SceneManager.LoadScene("LevelOne");
    }

    public void OnCreditsButton() {
        creditsUi.SetActive(true);
    }

    public void OnCloseButton() {
        creditsUi.SetActive(false);
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
