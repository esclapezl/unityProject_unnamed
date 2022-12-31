using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 0.2f;

    public void shake()
    {
        StartCoroutine(shaking());
    }

    public IEnumerator shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float strenght = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strenght * 0.05f;
            yield return null;
        }

        transform.position = startPosition;
    }
}
