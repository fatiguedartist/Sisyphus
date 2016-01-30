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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
            zDegree += 90f;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            zDegree -= 90f;

        if (Input.GetKeyUp(KeyCode.UpArrow))
            yDegree += 90f;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            yDegree -= 90f;

        if (Input.GetKeyUp(KeyCode.Q))
            xDegree += 90f;
        if (Input.GetKeyUp(KeyCode.E))
            xDegree -= 90f;


        zAngle = Mathf.LerpAngle(transform.rotation.z, zDegree, Time.deltaTime * 10);
        yAngle = Mathf.LerpAngle(transform.rotation.y, yDegree, Time.deltaTime * 10);
        xAngle = Mathf.LerpAngle(transform.rotation.x, yDegree, Time.deltaTime * 10);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xDegree, yDegree, zDegree), Time.deltaTime * 10);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "zDegree" + zDegree.ToString());
        GUI.Label(new Rect(0, 50, 100, 100), "yDegree" + yDegree.ToString());
        GUI.Label(new Rect(0, 100, 100, 100), "zAngle" + zAngle.ToString());
        GUI.Label(new Rect(0, 150, 100, 100), "yAngle" + yAngle.ToString());
    }

}
