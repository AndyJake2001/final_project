using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float turnSpeed = 100f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var forwardMotion = Input.GetAxis("Vertical");
        var turnMotion = Input.GetAxis("Horizontal");

        gameObject.transform.Translate(Vector3.forward * forwardMotion * speed * Time.deltaTime);

        var rotMult = forwardMotion >= 0 ? 1 : -1;
        gameObject.transform.Rotate(Vector3.up * rotMult * turnMotion * forwardMotion * turnSpeed * Time.deltaTime);
    }
}
