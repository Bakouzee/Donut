using System;
using System.Collections;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour {

    public enum ShakeObjectType {
        TREE,
        BUSH
    }
    
    private float timer;
    private Vector3 startPosition;

    public ShakeObjectType shakeType;
    public bool canShake;
    public bool isShaking;

    public void Shake(float time) {
        if (!canShake || isShaking) 
            return;

        startPosition = transform.position;
        isShaking = true;
        StartCoroutine(DoShake(time));
    }

    private IEnumerator DoShake(float time) {
        timer = 0f;

        while (timer < time) {
            timer += Time.deltaTime;
            Vector3 randomPos = startPosition + (Vector3)UnityEngine.Random.insideUnitCircle * 0.1f;

            transform.position = new Vector3(randomPos.x, transform.position.y, transform.position.z);
            yield return null;
        }

        transform.position = startPosition;

        StartCoroutine(EndShake(time));
    }

    private IEnumerator EndShake(float time) {
        yield return new WaitForSeconds(2f);
        isShaking = false;
        
        Shake(time);
    }
}
