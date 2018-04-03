using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnClick : MonoBehaviour {

    public CameraZoom zoom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        zoom.ZoomIn();
    }
}
