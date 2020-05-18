using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerDetector : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    public bool hasTriggered = false;

    [SerializeField]
    private ProBuilderMesh faceMesh;

    private BoxCollider playerBox;
    private BoxCollider targetBox;

    // Start is called before the first frame update
    void Start() {
        playerBox = player.GetComponent<BoxCollider>();
        targetBox = gameObject.GetComponent<BoxCollider>();

        // Highlight the player detector.
        foreach (var face in faceMesh.faces) {
            faceMesh.SetFaceColor(face, Color.green);
        }
        faceMesh.Refresh(RefreshMask.All);
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerStay(Collider other) {
        var bounds = targetBox.bounds;

        // Detect whether the player is within the bounds
        // of the given detector. This goes from the minimum
        // bounds to maximum (i.e. from one vertex to another)
        // and checks if the whole car is contained within these bounds.
        var isWin = (bounds.Contains(other.bounds.min) &&
            bounds.Contains(other.bounds.max)
            && other.GetComponent<BoxCollider>() == playerBox);

        // If not triggered yet, trigger the winning scenario.
        if (isWin && !this.hasTriggered) {
            this.hasTriggered = true;

            // Unhighlight the player detector.
            foreach (var face in faceMesh.faces) {
                faceMesh.SetFaceColor(face, Color.white);
            }
            faceMesh.Refresh(RefreshMask.All);
        }
    }
}
