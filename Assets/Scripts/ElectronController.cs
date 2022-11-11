using System.Collections;
using System.Threading;
using System;
using UnityEngine;

public class ElectronController : MonoBehaviour
{
    [SerializeField]
    private static float _speed;
    public float Speed{
        get{return _speed;}
        set{ _speed = value; }
}


    private float horizontalAxis;
    private bool switchGravity;
    private bool doubleCharge; //Immunity for a period of time
    private Rigidbody2D electron;
    private ConstantForce2D electronForce;
    private Vector2 electronPosition;
    private Animator electronAnimator;
    private int gravityScale;
    private const int SECONDS_FOR_IMMUNITY = 5;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        electron = gameObject.GetComponent<Rigidbody2D>();
        electronForce = gameObject.GetComponent<ConstantForce2D>();  
        electronAnimator = gameObject.GetComponent<Animator> ();
    }

    void Start()
    {
        _speed = 5F;
        switchGravity = doubleCharge =false;  
        gravityScale = 1;
        electron.drag = 1F;
    }

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal"); 
        switchGravity = Input.GetKeyDown(KeyCode.Space);
        doubleCharge = Input.GetKeyDown(KeyCode.B);

            if (switchGravity){
                SwitchGravityRoutine();
            }
            else if(doubleCharge){
               StartCoroutine(DoubleChargeCoroutine(SECONDS_FOR_IMMUNITY)); //Immunity
            }

            MoveElectron();
    }
  
    private void MoveElectron(){
    if (horizontalAxis > 0)
    {
        electron.AddForce(Vector2.right * _speed);
        electron.gravityScale = 0;
    }
    else if (horizontalAxis < 0)
    {
        electron.AddForce(Vector2.left * _speed);
        electron.gravityScale = 0;
    }
    else
     {
         electron.gravityScale = gravityScale;
     }
    //Update the constant force
    //Set the electron constant force to act on its new position
    electronForce.relativeForce.Set(electron.position.x,electron.position.y);
    }
    private void SwitchGravityRoutine(){
        ///This gameObject has an Animator property called gravityUp
                ///The line below sets it to true or false depending on whether the spacebar is hit or screen is tapped
                electronAnimator.SetBool("gravityUp",(gravityScale>0)?true:false);
                //Switch gravity
                gravityScale *= -1;
                electron.gravityScale = gravityScale;
    }

    private IEnumerator DoubleChargeCoroutine(int timeInSeconds){
       //Increase electron size 
        Vector3 electronScale = electron.transform.localScale;
        electronScale.x *=2;
        electronScale.y *=2;
        electron.transform.localScale = electronScale;

        //Start the Immunity animation
       // electronAnimator.SetBool("doubleCharge",true);
        

        //Wait
        yield return new WaitForSeconds(timeInSeconds);

        //End the Immunity animation
       // electronAnimator.SetBool("doubleCharge",false);

        //Reduce electron size
        electronScale.x /=2;
        electronScale.y /=2;
        electron.transform.localScale = electronScale;


    }
}
