using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithPlatform : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if(collision.transform.parent.gameObject == GameManager.instance.playerA.gameObject || collision.transform.parent.gameObject == GameManager.instance.playerB.gameObject)
        {
            collision.transform.parent.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject == GameManager.instance.playerA.gameObject || collision.transform.parent.gameObject == GameManager.instance.playerB.gameObject)
        {
            collision.transform.parent.transform.SetParent(null);
        }
    }
}
