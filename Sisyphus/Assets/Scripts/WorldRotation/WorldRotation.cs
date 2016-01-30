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
        Debug.Log(PlayerLocation.transform.localRotation);

        if (Input.GetKeyUp(KeyCode.LeftControl))
            HandleBackRotation();
    }

    private void HandleBackRotation()
    {
        if (PlayerLocation.transform.eulerAngles.y > 315 || PlayerLocation.transform.eulerAngles.y < 45)
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), 90);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 45 && PlayerLocation.transform.eulerAngles.y < 135)
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 0, 1), -90);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 135 && PlayerLocation.transform.eulerAngles.y < 225)
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(1, 0, 0), -90);
        }
        else if (PlayerLocation.transform.eulerAngles.y > 225 && PlayerLocation.transform.eulerAngles.y < 315)
        {
            transform.RotateAround(PlayerLocation.transform.position, new Vector3(0, 0, 1), 90);
        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 100), "PlayerLocation.transform.localRotation: " + PlayerLocation.transform.localRotation);
        GUI.Label(new Rect(0, 50, 300, 100), "PlayerLocation.transform.rotation: " + PlayerLocation.transform.rotation);
        GUI.Label(new Rect(0, 100, 300, 100), "PlayerLocation.transform.eulerAngles: " + PlayerLocation.transform.eulerAngles);
    }

}
