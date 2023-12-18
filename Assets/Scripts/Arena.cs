using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Arena : MonoBehaviour
{
    #region Global Variables
    [Header("Arena")]
    [Range(1.0f, 10.0f)][SerializeField] private float size;
    private GameObject ground;
    private GameObject ceiling;
    private GameObject wallsParent;
    private GameObject[] walls = new GameObject[4];
    private float boundPositionFromCenter;

    [Header("Platforms")]
    [SerializeField] private GameObject platform;   //Prefab of obstacle

    [Range(4, 20)][SerializeField] private int platformCount;
    [Range(3, 10)][SerializeField] private int platformSize;
    private List<GameObject> platforms = new List<GameObject>();


    [Header("Enemies")]
    [SerializeField] private GameObject enemy;
    [Range(4, 20)][SerializeField] private int enemyCount;
    [Range(1, 5)][SerializeField] private int enemySize;
    private List<GameObject> enemies = new List<GameObject>();

    [Header("Collectibles")]
    [SerializeField] private GameObject collectible;
    [Range(4, 20)][SerializeField] private int collectibleCount;
    [Range(1, 5)][SerializeField] private int collectibleSize;
    private List<GameObject> collectibles = new List<GameObject>();
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

    void Start()
    {
        boundPositionFromCenter = size * 10 / 2;    //distance to walls/borders from center

        ChangeSize();   //change size of all arena objects
        InstantiateObjects(platform, platformCount, transform.GetChild(3).transform, new Vector3(platformSize, .5f, platformSize), platforms);    //Instatiano of platforms
        InstantiateObjects(enemy, enemyCount, transform.GetChild(4).transform, new Vector3(enemySize, enemySize, enemySize), enemies);  //Instantiation of enemies
        InstantiateObjects(collectible, collectibleCount, transform.GetChild(5).transform, new Vector3(collectibleSize, collectibleSize, collectibleSize), collectibles);  //Instantiation of collectibles
    }

    void Update()
    {

    }

    #region Setup Arena
    private void ChangeSize()
    {
        //Ground and Ceiling
        ground.transform.localPosition = new Vector3(0, -1, 0) * boundPositionFromCenter;
        ground.transform.localScale = new Vector3(size, 1, size);

        ceiling.transform.localPosition = new Vector3(0, 1, 0) * boundPositionFromCenter;
        ceiling.transform.localScale = new Vector3(size, -1, size);

        //Walls
        float originalWallCenterOffset = walls[0].transform.localPosition.y; //walls are all same size, for are size = 1 wallSize = 5, y value is present for every wall

        Vector3 normalizedPosition = wallsParent.transform.localPosition / originalWallCenterOffset;    //get normalized position eg. Vector3 with only zeros and 1 or -1 
        wallsParent.transform.localPosition = normalizedPosition * boundPositionFromCenter; //multiply by position offset from center to move it to that position

        foreach (GameObject wall in walls)
        {
            wall.transform.localScale = new Vector3(size, wall.transform.localScale.y, size);   //change scale

            normalizedPosition = wall.transform.localPosition / originalWallCenterOffset;   //get normalized position eg. Vector3 with only zeros and 1 or -1 
            wall.transform.localPosition = normalizedPosition * boundPositionFromCenter;    //multiply by position offset from center to move it to that position
        }
    }
    private void InstantiateObjects(GameObject prefab, int count, Transform parent, Vector3 localScale, List<GameObject> list)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject instantiated = Instantiate(prefab, new Vector3(GetValueInBounds(), GetValueInBounds(), GetValueInBounds()), Quaternion.identity, parent);
            instantiated.transform.localScale = localScale;

            list.Add(instantiated);
        }
    }

    private void DeleteObstacles()
    {
        for (int i = 0; i < platformCount; i++)
            platforms.Clear();
    }
    #endregion

    #region General Functions
    private float GetValueInBounds()
    {
        return Random.Range(-boundPositionFromCenter * .9f, boundPositionFromCenter * .9f);   // multiplier to be more towards center
    }

    public float GetBoundsDistance()
    {
        return boundPositionFromCenter; //distance of wall from center
    }
    #endregion
}
