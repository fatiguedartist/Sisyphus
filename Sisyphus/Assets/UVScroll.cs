using UnityEngine;
using System.Collections;

public class UVScroll : MonoBehaviour
{
    private Material mat;
    public string textureToOffset = "_DetailAlbedoMap";

	// Use this for initialization
	void Start ()
	{
	    mat = GetComponent<MeshRenderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
	    mat.SetTextureOffset(textureToOffset, Vector2.right * Time.time * 0.1f);
	}
}
