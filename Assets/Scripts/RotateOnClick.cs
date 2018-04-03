using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class RotateOnClick : MonoBehaviour {

    public enum Axis {X,Y,Z};
    public int closedRotation;
    public int openRotation;
    public CameraZoom zoom;
	public float speed = 3f;
    public float threshold = 0.0001f;
    public List<GameObject> children; 
    public Axis axis;

    Camera m_MainCamera;
    
    private IEnumerator rotator;

	// Use this for initialization
	void Start () {
        m_MainCamera = Camera.main;

        int index = (int)axis;

        Vector3 angle = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        angle[index] = closedRotation;
        transform.eulerAngles = angle;

        rotator = PerformRotate(openRotation);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                foreach (GameObject child in children) {

                    if (hit.collider == child.GetComponent<Collider>()) {
						StartCoroutine(rotator);
                        if (zoom != null) {
                            zoom.ZoomIn();
                        }
                    }
                }
            }
        }
	}

    private IEnumerator PerformRotate(float end) {

        int index = (int)axis;

        //Vector3 euler = new Vector3(0,0,0);

        Vector3 euler = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        euler[index] = -end;
        Quaternion target = Quaternion.Euler(euler);

        while (Math.Abs(transform.rotation.eulerAngles[index] - end) > threshold) {

            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);
            yield return null;
        }


        Vector3 angle = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        angle[index] = end;
        transform.eulerAngles = angle;

        yield return null;
    }

}
