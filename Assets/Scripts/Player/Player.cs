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

        Quaternion rotation = transform.rotation;
        rotation = Quaternion.Euler(0f, rotation.y + Time.deltaTime * rotationSpeed * mouseHorizontal, 0f);
        transform.rotation = rotation;
        
        if (Mathf.Sqrt(horizontal * horizontal + vertical * vertical) > 0) {
            float movementAngle = Mathf.Atan2(-vertical, horizontal) + rotation.y;
            
            Vector3 position = transform.position;
            position = position + Quaternion.Euler(0f, movementAngle * Mathf.Rad2Deg + rotation.y, 0f) * Vector3.right * movementSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
}