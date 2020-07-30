using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public GameObject bulletManager;

    public GameObject rearThruster;
    public GameObject frontLeftThruster;
    public GameObject frontRightThruster;
    public GameObject leftThruster;
    public GameObject rightThruster;
    public GameObject frontThruster;
   
    GameObject[] thrusterArr;
    AudioSource aud;
    bool dead;

    int score;

    public AudioClip explodeClip;
    public AudioClip laserClip;
    
    public GameObject deadShip;

    public TextMeshProUGUI scoreCard;
    public ScoreScript scoreScript;
    
    public float deadTime;

    void Start()
    {
        score=0;
        Physics2D.IgnoreLayerCollision(8,10,true);
        dead=false;
        cam=Camera.main;
        aud=GetComponent<AudioSource>();
        thrusterArr=new GameObject[]{rearThruster,rightThruster,frontThruster,leftThruster};

        //scoreScript.ResetScore();
        //Application.targetFrameRate = 30;
    }
    float horizontalSpeed=5.0f;//5
    float verticalSpeed=5.0f;//5
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cam.rect);
        Vector2 mousePos;
        Vector3 point = new Vector3();
        if(Input.GetKeyDown("escape")){
                //TogglePause();
            }
        if(!dead&&Time.timeScale!=0f){
            HorizontalMovement(Input.GetAxis("Horizontal"));

            VerticalMovement(Input.GetAxis("Vertical"));
            
            
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;

            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            
            transform.rotation = Quaternion.LookRotation(transform.forward, point - transform.position);
            ThrustDecider();
        }
        if (dead && Time.time - deadTime > 1)
        {
            SceneManager.LoadScene("SampleScene");
        }
        
    }

    void TogglePause(){
        if(Time.timeScale==0f){
            Time.timeScale=1f;
        }
        else{
            Time.timeScale=0f;
        }

    }

    void HorizontalMovement(float m){
        if(Mathf.Abs(m)>0.1){
            transform.Translate(Vector3.right * Time.deltaTime*m*horizontalSpeed,Space.World);
        }
    }

    void ThrustDecider(){
        float horizontalThrust=Input.GetAxis("Horizontal");
        float verticalThrust=Input.GetAxis("Vertical");

        Vector3 myMove=transform.InverseTransformDirection(Vector3.right*horizontalThrust+Vector3.up*verticalThrust);
        bool[] fireThrusts = new bool[]{myMove.y>0.50f,myMove.x<-0.50f,myMove.y<-0.50f,myMove.x>0.50f};
        
        for(int i=0;i<fireThrusts.Length;i++){
            if(fireThrusts[i]){
                thrusterArr[i].SetActive(true);
            }
            else{
                thrusterArr[i].SetActive(false);
            }
        }

    }

    void VerticalMovement(float m){
        if(Mathf.Abs(m)>0.1){
            transform.Translate(Vector3.up * Time.deltaTime*m*verticalSpeed,Space.World);
        }
    }

    void Fire()
    {
        aud.clip=laserClip;
        aud.Play();
        //Debug.Log("Pew");
        bulletManager.GetComponent<BulletManager>().Fire(gameObject);
    }

    public void Score(int p){
        //score+=p;
        //scoreCard.text="Score: "+score;
        scoreScript.Score(p);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            Die();
        }
    }

    public void Die(){
        dead=true;
        //Debug.Log("I'm Dead");
        aud.clip=explodeClip;
        aud.Play();
        GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Rigidbody2D>().velocity=new Vector3(0f,0f,0f);
        GetComponent<PolygonCollider2D>().enabled=false;
        //transform.GetComponentInChildren<SpriteRenderer>().enabled=(false);
        //transform.GetComponentInChildren<Animator>().enabled=(false);
        foreach (GameObject child in thrusterArr)
        {
            if(child.GetComponent<SpriteRenderer>()){
                child.GetComponent<SpriteRenderer>().enabled = (false);
                child.GetComponent<Animator>().enabled=(false);
            }
            else{
                child.GetComponentInChildren<SpriteRenderer>().enabled = (false);
                child.GetComponentInChildren<Animator>().enabled=(false);
            }
            

        }
        GetComponent<SpriteRenderer>().enabled=(false);
        GetComponent<PolygonCollider2D>().enabled=false;
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(deadShip,transform.position,Quaternion.identity);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        deadTime = Time.time;
    }
}
