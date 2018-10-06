using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class calibrationNavigation : MonoBehaviour {

    [SerializeField]
    [Tooltip("Drag in the object that should become selected first for controller navigation")]
    private GameObject firstSelected = null;

    private void OnEnable()
    {
        if(firstSelected)
        {
            GameManager.instance.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = firstSelected;
        }
    }

}
