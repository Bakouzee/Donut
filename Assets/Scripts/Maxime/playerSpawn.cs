using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        /*if (SceneManager.GetActiveScene().name == "GrotteLD")
        {
            StartCoroutine(GameManager.Instance.PlayDoorCinematic());
        }*/
    }
}
