﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public Camera camera;
    public float moveSpeed = 3;
    public float rotSpeed = 3;

    public float positionThreshold = 0.0001f;
    public float rotationThreshold = 0.001f;

    public GameObject zoomMarker;

    private IEnumerator transformer;
    private IEnumerator rotator;

    private static Coroutine tRoutine;
    private static Coroutine rRoutine;

    private IEnumerator Translate(Vector3 to) {

        while (Vector3.Distance(camera.transform.position, to) > positionThreshold) {

            Vector3 start = camera.transform.position;
            camera.transform.position = Vector3.Lerp(start, to, moveSpeed*Time.deltaTime);

            yield return null;
        }

        camera.transform.position = to;

        yield return null;
    }

    private IEnumerator Rotate(Vector3 to) {

        Quaternion target = Quaternion.Euler(to);

        while (Quaternion.Angle(camera.transform.rotation, target) > rotationThreshold) {

            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, target, rotSpeed*Time.deltaTime);
            yield return null;
        }


        camera.transform.eulerAngles = to;

        yield return null;
    }

    public void ZoomIn() {

        Vector3 positionEnd = zoomMarker.transform.position;
        Vector3 rotationEnd = zoomMarker.transform.eulerAngles;

        if (tRoutine != null) {
            StopCoroutine(tRoutine);
            tRoutine = null;
        }

        if (rRoutine != null) {
            StopCoroutine(rRoutine);
            rRoutine = null;
        }

        tRoutine = StartCoroutine("Translate", positionEnd);
        rRoutine = StartCoroutine("Rotate", rotationEnd);
    }
}
