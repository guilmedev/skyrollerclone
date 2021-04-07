using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;

    [SerializeField]
    private bool shouldMove;

    [SerializeField]
    private float sensibility;

    [SerializeField]
    Rigidbody rB;

    [SerializeField]
    BoxCollider myCollider;

    [Header("CAMs")]
    [SerializeField]
    private GameObject winCam;

    [Header("RIG")]
    //RIG
    [SerializeField]
    RigBuilder rigBuilder;

    [SerializeField]
    Rig rigController;

    [SerializeField]
    GameObject[] trails;

    private const string PLAYER_SPEED_STRING = "Player_speed";



    private AnimationController animatorController;


    private void Awake()
    {
        rB          = GetComponent<Rigidbody>();
        //myCollider  = GetComponent<BoxCollider>();
        animatorController = GetComponentInChildren<AnimationController>();

        if (PlayerPrefs.HasKey(PLAYER_SPEED_STRING))
        {
            //Debug.Log("Plyer speed");
            var speedAmout = PlayerPrefs.GetFloat(PLAYER_SPEED_STRING, 0f );

            if (speedAmout > 0f)
            {
                //update player velocity
                playerSpeed += speedAmout;                
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    float horizontalInput = 0;
    [SerializeField]
    float rigValue;

    public Slider slidervalue;
    
    // Update is called once per frame
    void Update()
    {

        if(rigController)
        {
            if (slidervalue)
                rigController.weight = slidervalue.value;
        }

        if (shouldMove)
        {           
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
        }


    }

    public void InitGame()
    {
        this.shouldMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (GameController.instance.GameOver) return;


        if (other.gameObject.CompareTag("Respawn"))
        {
            playerSpeed += .08f;
            MeshRenderer[] meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();

            foreach (var mesh in meshes)
            {
                mesh.material.color = Color.green;
            }
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            //Die
            shouldMove = false;
            animatorController.SetDeathAnim();
            EnablePhysics();

            GameController.instance.SetGameOver();

            PlayerPrefs.DeleteKey(PLAYER_SPEED_STRING);

        }

        if (other.gameObject.CompareTag("Finish"))
        {
            //Won
            shouldMove = false;

            animatorController.SetWinAnim();


            var speedAmout = PlayerPrefs.GetFloat(PLAYER_SPEED_STRING , 0);
            speedAmout += .5f;
            PlayerPrefs.SetFloat(PLAYER_SPEED_STRING, speedAmout);
            PlayerPrefs.Save();


            if (winCam)
                winCam.SetActive(true);

            GameController.instance.SetGameWin();

        }
    }

    private void SetTrailsVisibility( bool visible)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].SetActive(visible);
        }
    }

    private void EnablePhysics()
    {
        rB.isKinematic = false;
        rB.AddForce ( new Vector3( 0 ,1f , .75f ) , ForceMode.Impulse );
        myCollider.enabled = true;
        rigBuilder.enabled = false;

        //Disable trails
        SetTrailsVisibility(false);
    }
    private void DisablePhysics()
    {
        rigBuilder.enabled = true;
        rB.isKinematic = true;        
        myCollider.enabled = false;
    }

}
