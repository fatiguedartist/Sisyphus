using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sisyphus;

public class FadeFromBlack : MonoBehaviour {

    public Image target;
	// Use this for initialization
	void Start () {
        if (GameState.Instance.PlayerDiedRecently)
        {
            target.color = Color.red;
        }
        else
        {
            target.color = Color.black;
        }
        
        target.CrossFadeAlpha(0, 2f, false);
	}
	
}
