using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArenaGenerator : MonoBehaviour
{

    /*
     * ATTEMPT TO MAKE CHANGING SIZE VISUALIZE IN EDITOR, IT WORKED FOR A WHILE BUT NOW IT THROWS ERROR!
     * 
     * !!!!!!!!!!!!!!!!!!!!!!!
     */

    #region Global Variables
    [Range(1.0f, 10.0f)][SerializeField] private float size;

    private GameObject ground;
    private GameObject ceiling;
    private GameObject wallsParent;
    private GameObject[] walls = new GameObject[4];

    private float boundPositionFromCenter;
    #endregion

    private void Awake()
    {
        ground = transform.GetChild(0).gameObject;
        ceiling = transform.GetChild(1).gameObject;
        wallsParent = transform.GetChild(2).gameObject;
        //Get all borders
        for (int i = 0; i < walls.Length; i++)
            walls[i] = wallsParent.transform.GetChild(i).gameObject;
    }
    private void OnValidate()
    {
        ChangeSize();   //change size of all arena objects
    }

    private void ChangeSize()
    {
        boundPositionFromCenter = size * 10 / 2;    //distance to walls/borders from center

        //Ground and Ceiling
        ground.transform.localPosition = new Vector3(0, -1, 0) * boundPositionFromCenter;
        ground.transform.localScale = new Vector3(size, 1, size);

        ceiling.transform.localPosition = new Vector3(0, 1, 0) * boundPositionFromCenter;
        ceiling.transform.localScale = new Vector3(size, -1, size);

        //Walls
        float originalWallCenterOffset = walls[0].transform.localPosition.y; //walls are all same size, for are size = 1 wallSize = 5, y value is present for every wall
        //float originalWallCenterOffset = 5f;
        //Debug.Log("originalWallCenterOffset: " + originalWallCenterOffset);

        Vector3 normalizedPosition = wallsParent.transform.localPosition / originalWallCenterOffset;    //get normalized position eg. Vector3 with only zeros and 1 or -1 
        wallsParent.transform.localPosition = normalizedPosition * boundPositionFromCenter; //multiply by position offset from center to move it to that position

        foreach (GameObject wall in walls)
        {
            wall.transform.localScale = new Vector3(size, wall.transform.localScale.y, size);   //change scale

            normalizedPosition = wall.transform.localPosition / originalWallCenterOffset;   //get normalized position eg. Vector3 with only zeros and 1 or -1 
            wall.transform.localPosition = normalizedPosition * boundPositionFromCenter;    //multiply by position offset from center to move it to that position
        }

    }


    public float GetBoundsDistance()
    {
        return boundPositionFromCenter; //distance of wall from center
    }
}
