using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalingPlattform : MonoBehaviour
{
    [SerializeField]
    private GameObject visualRepresentation = null;
    [SerializeField]
    [Tooltip("Speed of the animation")]
    [Range(0.95f, 0.997f)]
    private float scalingSpeed = 0.9f;
    [SerializeField]
    [Tooltip("How small should the object shrink")]
    private float minimumSize = 0.1f;
    [SerializeField]
    [Tooltip("Should the scaling begin on X or Y axis")]
    private bool bScaleOnYFirst = false;

    private bool bScaleOnX = true;
    private bool bShouldGrow = false;

    private void Start()
    {
        if (!bScaleOnYFirst)
            visualRepresentation.transform.localScale = new Vector3(1, minimumSize, 1);
        else
        {
            visualRepresentation.transform.localScale = new Vector3(minimumSize, 1, 1);
            bScaleOnX = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 scaleVector = Vector3.zero;
        float currentScale = 0;
        if (bScaleOnX)
        {
            scaleVector = visualRepresentation.transform.localScale;
            currentScale = visualRepresentation.transform.localScale.x;
            if (bShouldGrow)
            {
                if (currentScale < 1)
                    visualRepresentation.transform.localScale = new Vector3(currentScale * (1 / scalingSpeed), scaleVector.y, scaleVector.z);
                else
                    bShouldGrow = false;
            }
            else
            {
                if (currentScale > minimumSize)
                    visualRepresentation.transform.localScale = new Vector3(currentScale * scalingSpeed, scaleVector.y , scaleVector.z);
                else
                {
                    bScaleOnX = false;
                    bShouldGrow = true;
                }
            }
        }
        else
        {
            scaleVector = visualRepresentation.transform.localScale;
            currentScale = visualRepresentation.transform.localScale.y;
            if (bShouldGrow)
            {
                if (currentScale < 1)
                    visualRepresentation.transform.localScale = new Vector3(scaleVector.x, currentScale * (1 / scalingSpeed), scaleVector.z);
                else
                    bShouldGrow = false;
            }
            else
            {
                if (currentScale > minimumSize)
                    visualRepresentation.transform.localScale = new Vector3(scaleVector.x, currentScale * scalingSpeed, scaleVector.z);
                else
                {
                    bScaleOnX = true;
                    bShouldGrow = true;
                }
            }
        }
    }
}
