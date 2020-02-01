using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float movementSpeed = 5f;
    public float rotationSpeed = 1f;

    void Start() {
        
    }

    void Update() {
        Transform transform = GetComponent<Transform>();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");

        horizontal = ((horizontal > 0) ? 1 : ((horizontal < 0) ? -1 : 0));
        vertical = ((vertical > 0) ? 1 : ((vertical < 0) ? -1 : 0));

        Quaternion rotation = transform.rotation;
        rotation = rotation * Quaternion.Euler(0f, mouseHorizontal, 0f);
        transform.rotation = rotation;
        
        if (Mathf.Sqrt(horizontal * horizontal + vertical * vertical) > 0) {
            // degrees
            float movementAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg + rotation.eulerAngles.y;
            Vector3 position = transform.position;
            position = position + Quaternion.Euler(0f, movementAngle, 0f) * Vector3.right * movementSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
}