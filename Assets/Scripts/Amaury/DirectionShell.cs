using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionShell : MonoBehaviour {

    public float angularSpeed;
    public Player player;
    
    private float radius;
    private float angle;
    
    void Start() {
        Vector3 toShell = transform.position - player.transform.position;
        radius = 1;
        angle = Mathf.Atan2(toShell.y, toShell.x) * Mathf.Rad2Deg;
    }

    void Update() {
        if (player.isTransformed) {
            angle += angularSpeed * Time.deltaTime;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector3 toShell = new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad),0f) * radius;
            Vector3 arrowPosition = player.transform.position + toShell;

            transform.position = arrowPosition;
            transform.rotation = Quaternion.Euler(0,0, angle + 90);
        }

        GetComponent<SpriteRenderer>().enabled = player.isTransformed;
    }
}
