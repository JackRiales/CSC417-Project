using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public GameObject gun;
	public GameObject bullet;
	public float moveSpeed;
	public float shotForce;
	public float bulletLifeTime;
	private float gunAngle = 0.0f;

	protected void FixedUpdate() {
		GetComponent<Rigidbody> ().velocity = new Vector3 (
			Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
			GetComponent<Rigidbody> ().velocity.y,
			0
		);

		/*if (Input.GetKeyDown(KeyCode.Space))
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));*/

		// Rotate the gun around the player given the mouse position
		var pos = Camera.main.WorldToScreenPoint(transform.position);
		var dir = Input.mousePosition - pos;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// TODO not quite right here.
		gun.transform.RotateAround (transform.position, gun.transform.forward, angle);

		// Shoot bullets where the gun is pointing
		if (Input.GetMouseButtonDown (0)) {
			GameObject newBullet = (GameObject) Instantiate (bullet, gun.transform.position + gun.transform.right, Quaternion.identity);
			newBullet.GetComponent<Rigidbody>().AddForce(gun.transform.right * shotForce);
			GameObject.Destroy(newBullet, bulletLifeTime);
		}
	}
}
