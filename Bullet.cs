using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 350f;
    [SerializeField] float RotateSpeed = 2000f;
    public Rigidbody2D rb;
    public bool homing = false;
    public BulletManager bm;
    public bool toDestroy = false;
    public float offTime;
    public GameObject glint;
    public float tThreshold;
    public Sprite homingSprite;

    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    GameObject[] arrowOptions;
    GameObject currentArrow;
    Camera cam;

    Color redC;
    Color blueC;

    

    Vector3 screenPos;
    Vector2 onScreenPos;
    float max;
    // Start is called before the first frame update
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8,10,true);
        cam=Camera.main;
        arrowOptions=new GameObject[]{upArrow,downArrow,leftArrow,rightArrow};
        //67,121,48
        //0.26655,0.475,0.188

        //254,174,52
        //1,0.682,0.204
        redC=new Color(0.8686f,0.4672f,0.1333f);
        blueC=new Color(0.2627f,0.678f,0.9725f);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toDestroy == true && Time.time - offTime > tThreshold){
            SelfDestruct();
        }
        
        DrawArrow();
    }
    void DrawArrow(){
        Vector3 screenPos =cam.WorldToViewportPoint(transform.position);
        if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1){
            //Debug.Log("already on screen, don't bother with the rest!");
            if(currentArrow!=null){
                Destroy(currentArrow);
            }
            return;
        }
    
        onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos/(max*2))+new Vector2(0.5f, 0.5f); //undo mapping
        Vector3 worldPoint = cam.ViewportToWorldPoint(new Vector3(onScreenPos.x, onScreenPos.y, cam.nearClipPlane));

        //Debug.Log(worldPoint);
        int arrowIndex;
        Vector3 offset;
        if(onScreenPos.x>=1f){
                arrowIndex=3;
                offset=new Vector3(-0.2f,0f,0f);
            }
            else if(onScreenPos.x<=0f){
                arrowIndex=2;
                offset=new Vector3(0.2f,0f,0f);
            }
            else if(onScreenPos.y>=1f){
                arrowIndex=0;
                offset=new Vector3(0f,-0.2f,0f);
            }
            else{
                arrowIndex=1;
                offset=new Vector3(0f,0.2f,0f);
            }
        if(currentArrow!=null){
            currentArrow.transform.position=worldPoint+offset;
            currentArrow.GetComponent<SpriteRenderer>().sprite=arrowOptions[arrowIndex].GetComponent<SpriteRenderer>().sprite;
        }
        else{
            currentArrow=Instantiate(arrowOptions[arrowIndex],worldPoint+offset,Quaternion.identity);
            if(homing){
                currentArrow.GetComponent<SpriteRenderer>().color=redC;
            }
            else{
                currentArrow.GetComponent<SpriteRenderer>().color=blueC;
                
            }
            
        }
    }
    public void Home(GameObject target)
    {
        
        rb.velocity = transform.up * MoveSpeed * Time.deltaTime;

        if (homing == true){
            if(currentArrow!=null){
                currentArrow.GetComponent<SpriteRenderer>().color=redC;
            }
            Vector3 targetVector = target.transform.position - transform.position;

            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

            rb.angularVelocity = -1 * rotatingIndex * RotateSpeed * Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy1>().Hit();
        }
        else if (collision.gameObject.tag == "Player" && homing == true)
        {
            collision.gameObject.GetComponent<GameController>().Die();
        }
        else if (collision.gameObject.tag=="Shield")
        {
            Instantiate(glint,transform.position,Quaternion.identity);
            SelfDestruct();
        }
        else if (collision.gameObject.tag == "Reflector")
        {
            homing=true;
            transform.localRotation *= Quaternion.Euler(0, 0, 180);
            gameObject.layer=10;
            GetComponent<SpriteRenderer>().sprite = homingSprite;
            Instantiate(glint,transform.position,Quaternion.identity);
            return;
        }
        else if (collision.gameObject.tag == "Player" && homing == false)
        {
            //Debug.Log("Uh oh");
            return;
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Instantiate(glint, transform.position, Quaternion.identity);
            SelfDestruct();
        }
        else
        {
            Instantiate(glint, transform.position, Quaternion.identity);
            SelfDestruct();
        }
        SelfDestruct();
    }
    public void SelfDestruct(){
        bm.bullets.Remove(gameObject);
        if(currentArrow!=null){
            Destroy(currentArrow);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        toDestroy = true;
        offTime = Time.time;
    }

    void OnBecameVisible()
    {
        toDestroy = false;
    }
}
