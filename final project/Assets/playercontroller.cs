using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{


    public GameObject door;
    private float v;
    private float h;
    private float rotate, rotatey;
    private int speed = 3;
    private float speed2 = 0.5f;
    private Animator playeranimator;
    private Animator playershoot;
    public GameObject bullet;
    public Transform target;
    public AudioSource footstepsound;
    public int score = 0;
    private Boolean getcoin=false;
    public GameObject particaleffects;
    public GameObject coins;
    public Text scoretext;
    public Button button;
    void Start()
    {
        score = 0;
        playeranimator = this.GetComponent<Animator>();
        scoretext.text="score:"+score.ToString();
     //   particaleffects = GameObject.Find("star1");
    }


    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        rotate = speed * Input.GetAxis("Mouse X");
        rotatey = Input.GetAxis("Mouse Y");
        //jump = Input.GetAxis("Jump");
        transform.Translate(0, 0, v * Time.deltaTime * speed);
        transform.Rotate(rotatey * Time.deltaTime * speed2, rotate, 0);
        playeranimator.SetFloat("shouldwalk", v);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            footstepsound.enabled = true;
        }
        //else if (Input.GetKey(KeyCode.Space))
        //{
         //   footstepsound.enabled = false;
          //  transform.Translate(0, h * Time.deltaTime * speed,0);
      //  }

        else
        {
            footstepsound.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            playeranimator.SetBool("shoot", true);
            GameObject newbullet = Instantiate(bullet, target.position, Quaternion.identity);
            newbullet.GetComponent<Rigidbody>().AddForce(target.forward * 500);

        }

        else
        {
            playeranimator.SetBool("shoot", false);
        }
        
       

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "sensor")
        {
            door.gameObject.GetComponent<Animator>().SetBool("shouldopen", true);
        }

    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "sensor")
        {
            door.gameObject.GetComponent<Animator>().SetBool("shouldopen", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "treasure")
        {
            other.gameObject.GetComponent<Animator>().SetBool("open", true);


        }
        else if (other.gameObject.tag == "coin")
        {
            score = score + 20;
            if (score >= 90)
            {
                button.gameObject.SetActive( true);
            }
            print(score);
            scoretext.text ="score:" +score.ToString();

            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 3f);


            StartCoroutine(delay(other.gameObject));

            

        }
        else if(other.gameObject.tag == "silvercoins")
        {
            score = score + 10;
            if (score >= 90)
            {
                button.gameObject.SetActive(true);
            }
            print(score);
            scoretext.text = "score:" + score.ToString();

            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 3f);


            StartCoroutine(delay(other.gameObject));
        }
        else if (other.gameObject.tag == "copper")
        {
            score = score + 5;
            if (score >= 90)
            {
                button.gameObject.SetActive(true);
            }
            print(score);
            scoretext.text = "score:" + score.ToString();

            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 3f);


            StartCoroutine(delay(other.gameObject));
        }
        
    }

    public void levelup()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator delay(GameObject other)
    {
        yield return new WaitForSeconds(2f);
        GameObject obj = Instantiate(particaleffects, other.transform.position, other.transform.rotation);

        Destroy(obj, 5f);
    }
}
