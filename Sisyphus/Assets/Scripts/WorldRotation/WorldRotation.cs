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


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad6))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0,0,1), 90);
        if (Input.GetKeyUp(KeyCode.Keypad4))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 0, 1), -90);

        if (Input.GetKeyUp(KeyCode.Keypad8))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), 90);
        if (Input.GetKeyUp(KeyCode.Keypad2))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), -90);

        if (Input.GetKeyUp(KeyCode.Keypad1))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 1, 0), 90);
        if (Input.GetKeyUp(KeyCode.Keypad3))
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 1, 0), -90);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "zDegree" + zDegree.ToString());
        GUI.Label(new Rect(0, 50, 100, 100), "yDegree" + yDegree.ToString());
        GUI.Label(new Rect(0, 100, 100, 100), "zAngle" + zAngle.ToString());
        GUI.Label(new Rect(0, 150, 100, 100), "yAngle" + yAngle.ToString());
    }

}
