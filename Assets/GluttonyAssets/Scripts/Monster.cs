using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public Transform target;

	//to make things look more organized in unity
	[Header("Attributes")]

	public float range = 15f;
	public float fireRate =1f;
	private float fireCountdown = 0f;

	[Header("other stuff")]

	public float rotationSpeed = 35;

	public GameObject bulletPrefab;
	public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void UpdateTarget()
    {
    	GameObject player = GameObject.FindWithTag("MainCamera");
        float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);

        if (distanceToPlayer <= range){
        	target = player.transform;
          } else {
          	target = null;
          }
    }

    void Update(){
    	if (target == null)
    		return;
    	//{ transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);}
        else {transform.LookAt( new Vector3(target.position.x, transform.position.y, target.position.z)  );}

        if (fireCountdown <= 0f)
        {
        	Shoot();
        	fireCountdown = 1f /fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot ()
    {
    	//Debug.Log("shoot!");
    	GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
    	Bullet bullet = bulletGO.GetComponent<Bullet>();

    	if (bullet != null)
    		bullet.Seek(target);
    }

    void OnDrawGizmosSelected (){
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, range);
    }
    
}
