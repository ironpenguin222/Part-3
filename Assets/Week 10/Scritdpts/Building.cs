using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public GameObject[] buildingObjects;
    public float scaleDuration = 1f; 
    public float scaleAmount = 2f; 

    void Start()
    {
        StartCoroutine(ScaleBuilding());
    }

    IEnumerator ScaleBuilding()
    {
        foreach (GameObject obj in buildingObjects)
        {
            yield return StartCoroutine(ScaleObject(obj));
        }
    }

    IEnumerator ScaleObject(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;
        Vector3 targetScale = originalScale * scaleAmount;
        float Timer = 0f;

        while (Timer < scaleDuration)
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, Timer / scaleDuration);
            Timer += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }
}
