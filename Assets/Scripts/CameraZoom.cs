using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public Camera camera;
    public float moveSpeed;
    public float rotSpeed;

    public float positionThreshold;
    public float rotationThreshold;

    public GameObject zoomMarker;

    private IEnumerator transformer;
    private IEnumerator rotator;

    private static Coroutine tRoutine;
    private static Coroutine rRoutine;

    private IEnumerator Translate(Vector3 to) {
        Debug.Log("Start translating");

        while (Vector3.Distance(camera.transform.position, to) > positionThreshold) {

            Vector3 start = camera.transform.position;
            camera.transform.position = Vector3.Lerp(start, to, moveSpeed*Time.deltaTime);

            yield return null;
        }

        camera.transform.position = to;

        Debug.Log("Done translating");

        yield return null;
    }

    private IEnumerator Rotate(Vector3 to) {

        Debug.Log("Start rotating");

        Quaternion target = Quaternion.Euler(to);

        while (Quaternion.Angle(camera.transform.rotation, target) > rotationThreshold) {

            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, target, rotSpeed*Time.deltaTime);
            yield return null;
        }


        camera.transform.eulerAngles = to;

        Debug.Log("Done rotating");

        yield return null;
    }

    public void ZoomIn() {

        Vector3 positionEnd = zoomMarker.transform.position;
        Vector3 rotationEnd = zoomMarker.transform.eulerAngles;

        if (tRoutine != null) {
            Debug.Log("Stop translation early");
            StopCoroutine(tRoutine);
            tRoutine = null;
        }

        if (rRoutine != null) {
            Debug.Log("Stop rotation early");
            StopCoroutine(rRoutine);
            rRoutine = null;
        }

        tRoutine = StartCoroutine("Translate", positionEnd);
        rRoutine = StartCoroutine("Rotate", rotationEnd);
    }
}
