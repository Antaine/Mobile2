using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHive : MonoBehaviour
{
    public GameObject hive;
    public GameObject panel;
    public GameObject beePrefab;
    Vector2 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BeeMovement.hivePos = spawnPosition;
            Instantiate(hive, spawnPosition, Quaternion.Euler(new Vector2(0, 0)));
            this.GetComponent<SpawnHive>().enabled = false;
            panel.SetActive(false);
            SpawnBees();
        }
    }

    // Start is called before the first frame update
    public void SpawnBees()
    {
        for(int i =0;i<1;i++){
            GameObject bee = Instantiate(beePrefab, spawnPosition, transform.rotation);
            Time.timeScale = 1.0f;
        }    
    }
}
