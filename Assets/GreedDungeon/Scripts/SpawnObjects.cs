using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Coinprefab;
    public Vector3 center;
    public Vector3 size;
    private float countNum =1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) )
        {
            SpawnObject();
            //countNum++;
        }
        SpawnObject();
    }

    public void SpawnObject()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(Coinprefab, pos,Quaternion.identity);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = new Color(1, 0, 0, 0.5f);
    //    Gizmos.DrawCube(center, size);
    //}
}
