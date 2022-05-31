		using System.Collections;
		using System.Collections.Generic;
		using UnityEngine;
		
		public class GrapplingHook : MonoBehaviour
		{
		    
		    public LineRenderer Line;
		    DistanceJoint2D joint;
		    Vector3 targetPos;
		    RaycastHit2D hit;
		    public float distance = 10f;
		    public LayerMask Platform;
		    public float step = 0.05f;
		    
		    
		    // Start is called before the first frame update
		    void Start()
		    {
		        joint = GetComponent<DistanceJoint2D>();
		        joint.enabled = false;
		        Line.enabled = false;
		    }
		
		    // Update is called once per frame
		    void Update()
		    {
				
				if (joint.distance > 1f) {
					joint.distance -= step;
				}
				else {
					Line.enabled = false;
					joint.enabled = false;
				}
				
				
				
		        if (Input.GetKeyDown(KeyCode.C)) {
					targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					targetPos.z = -4;
					
					hit = Physics2D.Raycast(transform.position, targetPos-transform.position, distance, Platform);
					
					if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {
						joint.enabled = true;
						joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
						joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
						joint.distance = Vector2.Distance(transform.position, hit.point);
						
						Line.enabled = true;
						
						Line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -4));
						Line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -4));
						
						
					}
				}
				
				if (Input.GetKey(KeyCode.C)) {
					Line.SetPosition(0, transform.position);
				}
				
				if (Input.GetKeyUp(KeyCode.C)) {
					joint.enabled = false;
					Line.enabled = false;
		    }
		}
	}
		
	
