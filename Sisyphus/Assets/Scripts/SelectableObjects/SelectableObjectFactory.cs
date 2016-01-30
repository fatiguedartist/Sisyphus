using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectableObjectFactory : MonoBehaviour {

    public List<Selection> selectableObjectPrefabs;

    void Start()
    {
        SpawnObject(Vector3.zero);
    }

	public void SpawnObject(Vector3 spawnPosition)
    {
        //Create object
        var obj = selectableObjectPrefabs.SelectRandom().Instantiate();
        obj.transform.position = spawnPosition;
    }
}
