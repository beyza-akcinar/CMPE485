using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjectScript : MonoBehaviour
{
    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cubePrefab, transform.position, Quaternion.identity);
        }
    }
}
