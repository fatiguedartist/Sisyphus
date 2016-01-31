using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sisyphus;

public class FadeFromBlack : MonoBehaviour {

    public Image target;
    public List<Sprite> textures;

    public bool affectColor = true;
	// Use this for initialization
	void Start () {
	    if (affectColor)
	    {
            if (GameState.Instance.PlayerDiedRecently)
            {
                target.color = Color.red;
            }
            else
            {
                target.color = Color.black;
            }
        }
	    else
	    {
	        var index = GameState.Instance.Level - GameState.MinLevel;
	        if (index >= textures.Count - 1)
	        {
	            index = textures.Count - 1;
	        }

	        target.sprite = textures[index];
	    }
        
        
        target.CrossFadeAlpha(0, 2f, false);
	}
	
}
