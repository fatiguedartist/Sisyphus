using UnityEngine;
using System.Collections;

public class WorldRotation : MonoBehaviour
{
    private float zDegree;
    private float yDegree;
    private float xDegree;
    private float zAngle;
    private float yAngle;
    private float xAngle;

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

        if (PlayerLocation.transform.eulerAngles.y > 315 || PlayerLocation.transform.eulerAngles.y < 45)
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), targetAngle); // NORTH
        }
        else if (PlayerLocation.transform.eulerAngles.y > 45 && PlayerLocation.transform.eulerAngles.y < 135) // EAST 
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 0, 1), -targetAngle);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 135 && PlayerLocation.transform.eulerAngles.y < 225) // SOUTH
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), -targetAngle);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 225 && PlayerLocation.transform.eulerAngles.y < 315) // WEST
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 0, 1), targetAngle);
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
