using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour {

    public List<GameObject> possibleDirection;

    public void RandomActive()
    {
        foreach(GameObject go in possibleDirection)
        {
            foreach(Transform children in go.GetComponentsInChildren<Transform>())
            {
                children.gameObject.SetActive(true);
            }
            go.SetActive(false);
        }
        possibleDirection[Random.Range(0, possibleDirection.Count)].SetActive(true);
    }
}
