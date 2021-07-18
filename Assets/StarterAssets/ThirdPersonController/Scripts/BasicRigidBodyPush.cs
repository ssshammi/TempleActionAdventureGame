using StarterAssets;
using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
	public LayerMask pushLayers;
	public bool canPush;
	[Min(0.0f)] public float strength = 80.1f;

	public ForceMode ForceMode = ForceMode.Impulse;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		
		if (canPush) PushRigidBodies(hit);
	}
    private void OnTriggerEnter(Collider other)
    {
		var bodyLayerMask = other.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		this.transform.gameObject.GetComponent<ThirdPersonController>().PushObject(true);

	}

	private void OnTriggerExit(Collider other)
	{
		var bodyLayerMask = other.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		this.transform.gameObject.GetComponent<ThirdPersonController>().PushObject(false);

	}
	/*

	private void OnTrigger (Collision collision)
    {
		Debug.Log("colliosn enter");
		if(collision.gameObject.tag == "Pushable")
		{
			Debug.Log("colliosn enter inside if");
			this.transform.gameObject.GetComponent<ThirdPersonController>().PushObject(true);
		}
	}
    private void OnCollisionExit(Collision collision)
    {
		Debug.Log("colliosn exit");
		if (collision.gameObject.tag == "Pushable")
		{
			Debug.Log("colliosn exit inside if");
			//this.transform.gameObject.GetComponent<ThirdPersonController>().PushObject(false);
		}
	}
	*/
    private void PushRigidBodies(ControllerColliderHit hit)
	{
		return;
		// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

		// make sure we hit a non kinematic rigidbody
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null) return;
		//push animation 

		
		// make sure we only push desired layer(s)
		var bodyLayerMask = 1 << body.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		TransformWaypointsPathMovement twpm = hit.collider.GetComponentInParent<TransformWaypointsPathMovement>();

		Debug.Log("colliosn done");
		/*if (twpm != null)
		{
			twpm.Move(strength * Time.deltaTime, pushDir);
		}*/

		this.transform.gameObject.GetComponent<ThirdPersonController>().PushObject(true);
		// Apply the push and take strength into account
		body.AddForce(pushDir * strength, this.ForceMode);

		//Debug.Log("Pushed");
	}
}