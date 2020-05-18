using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    [SerializeField]
    private float penalty = 5.0f;

    private LevelManager manager;

    // Start is called before the first frame update
    void Start() {
        manager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnCollisionEnter(Collision collision) {
        // Check the other rigidbody's tag, to check if it's a player.
        if (collision.rigidbody != null && collision.rigidbody.CompareTag("Player")) {
            manager.remainingTime -= penalty;

            var sound = manager.crashSound;
            AudioSource.PlayClipAtPoint(sound, collision.transform.position);

        }
    }
}
