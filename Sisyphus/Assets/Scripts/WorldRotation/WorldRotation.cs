using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;

public class WorldRotation : MonoBehaviour
{
    public GameObject PlayerLocation;
    public Camera FPSView;

    private bool transitioning = false;

    void Update()
    {
        //Debug.Log(PlayerLocation.transform.localRotation);

        if (Input.GetMouseButtonDown(1) && !transitioning)
            HandleBackRotation();

        Debug.DrawRay(FPSView.transform.position, FPSView.transform.forward * 50, Color.green);
    }

    Vector3 Snap(Vector3 v3)
    {
        var possibleVectors = new List<Vector3>
        {
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down,
            Vector3.forward,
            Vector3.back,
        };

        var bestOption = possibleVectors.OrderBy(v => Quaternion.Angle(Quaternion.Euler(v), Quaternion.Euler(v3))).First();

        return bestOption;
    }

    private void HandleBackRotation()
    {
        int targetAngle = 90;
        var localEuler = FPSView.transform.localEulerAngles.x;
        if (localEuler > 180)
            localEuler -= 360;

        if (localEuler > 45 || localEuler < -45)
        {
            targetAngle = 180;
        }

        var axis = PlayerLocation.transform.right;
        axis = Snap(axis);
        RequestRotate(axis, -targetAngle);
    }

    private void RequestRotate(Vector3 axis, int targetAngle)
    {
        transitioning = true;
        var rigidBody = FPSView.transform.parent.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        StartCoroutine(rotate(axis, targetAngle));
    }

    private IEnumerator rotate(Vector3 axis, int targetAngle)
    {
        float currentAngle = 0;
        var recordedPosition = PlayerLocation.transform.position;

        float Multiplier = 300f * -targetAngle / 90f;
        while (Mathf.Abs(currentAngle) <= Mathf.Abs(targetAngle))
        {
            transform.RotateAround(recordedPosition, axis, Time.deltaTime * Multiplier);
            currentAngle += Time.deltaTime * Multiplier;
            Multiplier = Mathf.Lerp(Multiplier, 1, Time.deltaTime * 3);
            yield return new WaitForEndOfFrame();
        }

        ClampRotation(axis, targetAngle, currentAngle, recordedPosition);

        var rigidBody = FPSView.transform.parent.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;

        transitioning = false;
    }

    private void ClampRotation(Vector3 axis, int targetAngle, float currentAngle, Vector3 recordedPosition)
    {
        float rotateDiff = (Mathf.Abs(currentAngle) - Mathf.Abs(targetAngle));
        if (rotateDiff > 0)
        {
            if (targetAngle < 0)
            {
                transform.RotateAround(recordedPosition, axis, -rotateDiff);
            }
            else
            {
                transform.RotateAround(recordedPosition, axis, rotateDiff);
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 100), "PlayerLocation.transform.localRotation: " + PlayerLocation.transform.localRotation);
        GUI.Label(new Rect(0, 50, 300, 100), "PlayerLocation.transform.rotation: " + PlayerLocation.transform.rotation);
        GUI.Label(new Rect(0, 100, 300, 100), "PlayerLocation.transform.eulerAngles: " + PlayerLocation.transform.eulerAngles);

        GUI.Label(new Rect(0, 150, 300, 100), "FPSView.transform.eulerAngles: " + FPSView.transform.eulerAngles);
    }

}
