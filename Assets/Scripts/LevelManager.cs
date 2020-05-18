using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    private float startTime = 30.0f;

    public float remainingTime = -30f;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI spotsText;

    [SerializeField]
    private MenuManager menu;

    public AudioClip crashSound;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject[] winningDetectors;

    [SerializeField]
    private string nextLevel;

    private float lowestHeight = -10.0f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Handling the timer updates.
        if (startTime > 0 && !menu.isPaused) {
            if (remainingTime > 0) {
                // Subtract the time by the delta time to balance FPS.
                remainingTime -= Time.deltaTime;

                // Separate minute and second components of the time.
                var mins = (int)Mathf.Floor(remainingTime / 60);
                var secs = (int)(remainingTime - mins * 60);

                // Negative values mean 0.
                if (secs < 0 || mins < 0) {
                    secs = 0;
                    mins = 0;
                }

                // Pad with leading 0s to make it look like a real time.
                var minStr = mins.ToString("D2");
                var secStr = secs.ToString("D2");

                // Format the timer text.
                timerText.text = $"{minStr}:{secStr}";
            } else {
                // Activate lose condition.
                onLose();
                return;
            }
        }

        // Handling the remaining spots updates.
        var detectedSpots = 0;
        foreach (var goDetector in winningDetectors) {
            // Detect which spots have been activated.
            var detector = goDetector.GetComponent<PlayerDetector>();
            if (detector.hasTriggered) {
                detectedSpots++;
            }
        }

        spotsText.text = $"{detectedSpots}/{winningDetectors.Length} Spot(s)";

        if (detectedSpots == winningDetectors.Length) {
            // Activate win condition.
            onWin();
            return;
        }

        // Detect player.
        var playerBody = player.GetComponent<Rigidbody>();
        if (playerBody.position.y < lowestHeight) {
            onLose();
            return;
        }
    }

    private void onLose() {
        menu.triggerLose();
        Destroy(gameObject);
    }

    private void onWin() {
        menu.triggerWin(nextLevel);
        Destroy(gameObject);
    }
}
