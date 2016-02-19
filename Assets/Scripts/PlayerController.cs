using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
	public GUIText timeText;
	public AudioSource _1up_;
	public AudioSource _coin_;

	private int count;
	private int numberOfGameObjects;


	void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";
		numberOfGameObjects = GameObject.FindGameObjectsWithTag("PickUp").Length;
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
		timeText.text = "TIME: " + Time.time;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "PickUp")
		{
			other.gameObject.SetActive(false);
			//other.gameObject.transform.localPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0.5f, Random.Range(-5.0f, 5.0f)); 
			count = count + 1;
			SetCountText();
			//this.GetComponent<AudioSource> ().Play ();
			if (count >= numberOfGameObjects) {
				_1up_.Play ();
			} else {
				_coin_.Play ();
			}

		}
	}		

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Wall") {
			Color c = other.gameObject.GetComponent<Renderer> ().material.color;

			float r = 0.0f;
			float g = 0.0f;
			float b = 0.0f;
//			float b = Random.Range (0.0f, 1.0f);
			if (c.Equals(Color.white)) {
				r = 1.0f; g = 0.0f; b = 0.0f;
			} else if (c.Equals(Color.red)) {				
				r = 0.0f; g = 1.0f; b = 0.0f;
			} else if (c.Equals(Color.green)) {				
				r = 0.0f; g = 0.0f; b = 1.0f;
			} else if (c.Equals(Color.blue)) {		
				r = 1.0f; g = 1.0f; b = 1.0f;
			}

			other.gameObject.GetComponent<Renderer> ().material.color = new Color(r, g, b, 1.0f);
			//float lerp = Mathf.PingPong(Time.time, duration) / duration;
			//winText.text = "WALL";
		}
	}

	
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= numberOfGameObjects)
		{
			winText.text = "YOU WIN!";
		}
	}
}
