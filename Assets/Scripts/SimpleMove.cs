using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple movement script will move the object based on key input, in the direction the object is facing.
/// Requires XMove, YMove, and ZMove to be set up in the input manager.
/// </summary>

public class SimpleMove : MonoBehaviour {
    public Vector3 mov;
    public float speed = 2;


	void Update () {
        mov.Set(Input.GetAxis("XMove"), Input.GetAxis("YMove"), Input.GetAxis("ZMove"));
        transform.localPosition +=  transform.rotation * mov * Time.deltaTime * speed;
	}
}
