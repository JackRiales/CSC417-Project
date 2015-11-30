using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public GameObject gun;
	public GameObject bullet;
	public float moveSpeed;
	public float shotForce;
	public float bulletLifeTime;
	private Vector3 gunOffset;

	protected void Start() {
		gunOffset = (gun.transform.position - this.transform.position);
	}

	protected void Update() {
		// Rotate the gun around the player given the mouse position		
		Vector3 centerScreenPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - centerScreenPos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		gun.transform.position = transform.position + q * gunOffset;
		gun.transform.rotation = q;
	}

	protected void FixedUpdate() {
		// Move based on the horizontal input axis
		GetComponent<Rigidbody> ().velocity = new Vector3 (
			Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
			GetComponent<Rigidbody> ().velocity.y,
			0
		);

		// Jump functionality
		/*if (Input.GetKeyDown(KeyCode.Space))
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));*/

		// Shoot bullets where the gun is pointing
		if (Input.GetMouseButtonDown (0)) {
			GameObject newBullet = (GameObject) Instantiate (bullet, gun.transform.position + gun.transform.up, Quaternion.identity);
			newBullet.GetComponent<Rigidbody>().AddForce(gun.transform.up * shotForce);
			GetComponent<Rigidbody>().AddForce(-gun.transform.up * shotForce);
			GameObject.Destroy(newBullet, bulletLifeTime);
		}
	}
}
