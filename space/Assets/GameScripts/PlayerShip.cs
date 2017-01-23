// by piershaw 2010


using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : MonoBehaviour
{
	
	private Vector3 shipDirection;
	private Vector3 shipRotation;
	private Vector3 camRotation;
	private Vector3 laserDirection;
	
	private float camRot;
	private float shipPosX;
	private float shipPosY;
	//private float shipPosZ;
	private float shipSpeed = 0.8f;
	private float shipRotationSpeed = 0.8f;
	
	public ParticleEmitter pe;
	public GameObject cam;
	public GameObject gameArea;
	
	
	public GameObject smallLaser;
	private GameObject smallLaserClone;
	public List <GameObject> smallLaserList;

	private bool reloadLaser = false;
	private bool reloadStart = false;
	
	private int reloadTimer;
	private int reloadSpeed = 60;
	private int laserSpeed = 200;
	private int laserRange = 1;
	private int shipsEngines = 40;
	
	//private Ray ray;
	//private RaycastHit rayHit;
	
	
	
	
	
	
	public void Start(){
		reloadTimer = reloadSpeed;
		smallLaserList = new List<GameObject>();
	}

	
	public void Update(){
		
		shipPosX  = Input.GetAxis ("Vertical") * shipSpeed;
		shipPosY  = Input.GetAxis ("Horizontal")  * shipRotationSpeed;
		shipDirection.x = shipPosX;
		shipRotation.y = shipPosY;
		camRot = Input.GetAxis ("Horizontal") * shipRotationSpeed;
		camRotation.z = camRot;
		this.transform.Translate(shipDirection,Space.Self);
		this.transform.Rotate(shipRotation,Space.Self);
	    pe.maxSize = shipDirection.x * shipsEngines;
		cam.transform.Rotate(camRotation,Space.Self);
	
		//fire
		if(Input.GetMouseButtonDown(0) && reloadLaser != true){
	    smallLaserClone = (GameObject)Instantiate(smallLaser,this.transform.position,transform.rotation);
		smallLaserClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (-Vector3.left * laserSpeed);
		smallLaserList.Add(smallLaserClone);
		reloadStart = true;
		reloadLaser = true;
				
				
		}else if(Input.GetMouseButtonDown(1)){
			Debug.LogWarning("Fire Rockets");
		}else{
			
			//check laser ammo
			if(smallLaserList.Count > 10){
				reloadStart = false;
				reloadLaser = true;
				Debug.LogWarning("laser out!");
			}	
			
		}
		
		

		// timer
		if(reloadStart){
			reloadTimer -= 1;
			//pause
			if(reloadTimer < 1){
				reloadStart = false;
				reloadLaser = false;
				reloadTimer = reloadSpeed;
			}
		}
		
		
		//destroy
		if(smallLaserClone != null){
			Destroy(smallLaserClone, laserRange);
		}
		
		
		
		foreach(GameObject r in smallLaserList){
			
			if(r != null){
				
			if(r.GetComponent<Collider>() == HitDetection.hitName){
				Destroy(smallLaserClone);
			}
				
			}
			
		}
		
		
			//if(!smallLaserClone.collider.bounds.Intersects(gameArea.collider.bounds)){
		
			//}
	
		
	}// end
	
	
	
	
	
	
		
}//end of class


