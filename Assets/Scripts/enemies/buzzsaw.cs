using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buzzsaw : MonoBehaviour {

    public float moveForce = 10;
    public float waypointAffirmDeadzone = 0.01f;
    public bool shouldCircle = true;
    public Transform[] waypointArray;
    private List<Transform> waypointList = new List<Transform>();
    private int actualWaypointIndex = 1;
    private bool increase = true;

	// Use this for initialization
	void Start ()
    {
        //Transform[] waypointArrayCopyTemp = new Transform[waypointArray.Length + 1];
        Transform waypointInst = Instantiate(waypointArray[0]);
        waypointInst.transform.position = transform.position;
        waypointInst.transform.parent = transform.parent;
        waypointInst.name = "waypoint_spawnLoaction";

        waypointList.Add(waypointInst);
        foreach(Transform element in waypointArray)
        {
            waypointList.Add(element);
        }

        hideWaypointSprites();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        chooseNextWaypoint();
        moveBuzzsaw();
    }

    /// <summary>
    /// In case the currect waypoint is reached: choose the next one depending on given parameter
    /// </summary>
    void chooseNextWaypoint()
    {
        if (hasWaypointReached())
        {
            // In case we reached a waypoint, goTo the next waypoint
            if (increase)
                actualWaypointIndex++;
            else // backtracking only
                actualWaypointIndex--;

            // should the saw follow a circular path with its waypoints or should it backtrack the path?
            if (shouldCircle)
            {
                // In case the selected waypoint is outOfRange (positiv), go back to the fist waypoint
                if (actualWaypointIndex == waypointList.Count)
                {
                    actualWaypointIndex = 0;
                }
            }
            else
            {
                // In case the selected waypoint is outOfRange (positiv), reverse counting and backtrack the waypoints
                if (actualWaypointIndex == waypointList.Count)
                {
                    actualWaypointIndex = actualWaypointIndex - 2;
                    increase = false;
                }
            }

            // In case the selected waypoint is outOfRange (negative) reset it to 1 and count upwards
            if (actualWaypointIndex < 0)
            {
                actualWaypointIndex = 1;
                increase = true;
            }
        }
    }

    /// <summary>
    /// Returns true if the saw has reached a waypoint (including the deadzone as an offset)
    /// </summary>
    /// <returns></returns>
    bool hasWaypointReached()
    {
        if (
                transform.position.x < waypointList[actualWaypointIndex].transform.position.x + waypointAffirmDeadzone &&
                transform.position.x > waypointList[actualWaypointIndex].transform.position.x - waypointAffirmDeadzone &&
                transform.position.y < waypointList[actualWaypointIndex].transform.position.y + waypointAffirmDeadzone &&
                transform.position.y > waypointList[actualWaypointIndex].transform.position.y - waypointAffirmDeadzone
          )
            return true;
        else
            return false;
    }

    /// <summary>
    /// Move the Buzzsaw toward the actual waypoint with a given speed
    /// </summary>
    void moveBuzzsaw()
    {
        Vector3 directionVector = Vector3.Normalize(waypointList[actualWaypointIndex].transform.position - transform.position);
        transform.Translate(directionVector * (moveForce / 100));
    }

    /// <summary>
    /// Disable the SpriteRenderer of each waypoint, cause they should only be visible to the designer
    /// </summary>
    void hideWaypointSprites()
    {
        foreach(Transform element in waypointList)
        {
            SpriteRenderer eleSprite = element.GetComponent<SpriteRenderer>();
            if(eleSprite)
            {
                eleSprite.enabled = false;
            }
        }
    }
}
