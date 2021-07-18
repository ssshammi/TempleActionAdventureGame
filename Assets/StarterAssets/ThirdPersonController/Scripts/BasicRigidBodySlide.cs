using UnityEngine;
using StarterAssets;
public class BasicRigidBodySlide : MonoBehaviour
{
	public LayerMask SlopeLayers;
	public bool canSlide;
	[Min(0.0f)] public float strength = 10.1f;

	public ForceMode ForceMode = ForceMode.Impulse;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canSlide) PushRigidSlope(hit);
	}

	private void PushRigidSlope(ControllerColliderHit hit)
	{
		// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

		// make sure we hit a non kinematic rigidbody
		//Rigidbody body = hit.collider.attachedRigidbody;
		//if (body == null) return;

		// make sure we only push desired layer(s)
		var bodyLayerMask = hit.gameObject.layer;//1 << 
		if ((bodyLayerMask & SlopeLayers.value) == 0) return;

		// We dont want to push objects below us
		//if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 slopehDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, 0.0f);

		/*TransformWaypointsPathMovement twpm = hit.collider.GetComponentInParent<TransformWaypointsPathMovement>();

		if (twpm != null)
		{
			twpm.Move(strength * Time.deltaTime, pushDir);
		}*/
		this.transform.gameObject.GetComponent<ThirdPersonController>().SlidePlayer(true);
		// Apply the push and take strength into account
		//body.AddForce(pushDir * strength, this.ForceMode);

		Debug.Log("SlidePlayer");
	}
}