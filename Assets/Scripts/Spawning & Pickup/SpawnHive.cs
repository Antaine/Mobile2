using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHive : MonoBehaviour
{
    public GameObject hive;
    public GameObject panel;
    public GameObject beePrefab;
    public static Vector2 spawnPosition;
    private Vector2 moveDir;
    public static int score =0;
    public static List<GameObject> activeBees = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        score = 0;
    }
    //Updates Bees
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(hive, spawnPosition, Quaternion.Euler(new Vector2(0, 0)));
            this.GetComponent<SpawnHive>().enabled = false;
            panel.SetActive(false);
            SpawnBees();
            Time.timeScale = 1.0f; 
        }
    }
    //Spawns Bees
    public void SpawnBees()
    {
        for(int i =0;i<4;i++){
            GameObject bee = Instantiate(beePrefab, spawnPosition, transform.rotation);
            activeBees.Add(bee);  
        }   
    }
}
