using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject thisEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        thisEnemy.gameObject.transform.position = new Vector3(thisEnemy.gameObject.transform.position.x, thisEnemy.gameObject.transform.position.y -1f, 0f) ;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DISPARO")
        {
            FindObjectOfType<GameController>().GetComponent<GameController>().puntos++;
            Destroy(this.gameObject);
        }
    }

}
