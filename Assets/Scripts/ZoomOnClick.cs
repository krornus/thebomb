using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoomOnClick : MonoBehaviour {

    public CameraZoom zoom;

    void OnMouseDown() {
        Debug.Log(String.Format("Click on {0}", gameObject.name));
        zoom.ZoomIn();
    }
}
