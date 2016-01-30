using UnityEngine;
using System.Collections;

public class WorldRotation : MonoBehaviour
{
    public GameObject PlayerLocation;
    public Camera FPSView;


    void Update()
    {
        //Debug.Log(PlayerLocation.transform.localRotation);

        if (Input.GetMouseButtonDown(1))
            HandleBackRotation();

        Debug.DrawRay(FPSView.transform.position, FPSView.transform.forward * 50, Color.green);
    }

    private void HandleBackRotation()
    {
        int targetAngle = 90;
        if (FPSView.transform.eulerAngles.x >= 270 && FPSView.transform.eulerAngles.x <= 315)
        {
            targetAngle = 180;
        }

        if (PlayerLocation.transform.eulerAngles.y > 315 || PlayerLocation.transform.eulerAngles.y < 45) // NORTH
        {
            RequestRotate(new Vector3(1, 0, 0), targetAngle);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 45 && PlayerLocation.transform.eulerAngles.y < 135) // EAST 
        {
            RequestRotate(new Vector3(0, 0, 1), -targetAngle);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 135 && PlayerLocation.transform.eulerAngles.y < 225) // SOUTH
        {
            RequestRotate(new Vector3(1, 0, 0), -targetAngle);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 225 && PlayerLocation.transform.eulerAngles.y < 315) // WEST
        {
            RequestRotate(new Vector3(0, 0, 1), targetAngle);
        }

    }

    private void RequestRotate(Vector3 axis, int targetAngle)
    {
        StartCoroutine(rotate(axis, targetAngle));
        //transform.RotateAround(PlayerLocation.transform.position, axis, targetAngle);
    }

    private IEnumerator rotate(Vector3 axis, int targetAngle)
    {
        float currentAngle = 0;
        var recordedPosition = new Vector3(PlayerLocation.transform.position.x, PlayerLocation.transform.position.y, PlayerLocation.transform.position.z);

        float Multiplier = 300f;
        while (Mathf.Abs(currentAngle) <= Mathf.Abs(targetAngle))
        {
            transform.RotateAround(recordedPosition, axis, Time.deltaTime * Multiplier);
            currentAngle += Time.deltaTime * Multiplier;
            Multiplier = Mathf.Lerp(Multiplier, 1, Time.deltaTime * 3);
            yield return new WaitForEndOfFrame();
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
