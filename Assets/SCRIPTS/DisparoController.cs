using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoController : MonoBehaviour {

    public int tipo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && tipo == 1)
        {
            Destroy(collision.gameObject);
            FindObjectOfType<GameController>().GetComponent<GameController>().puntos++;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Enemy" && tipo == 2)
        {
            this.gameObject.GetComponent<CircleCollider2D>().radius = 50;

        } 
    }
}
