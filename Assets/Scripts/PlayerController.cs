using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // for TextMeshPro

public class PlayerController : MonoBehaviour
{
    private const int NUM_PICKUPS = 13;
    private const int DURATION = 8;
    private const float SPEED_CHANGE = 3;
    private Vector3 SCALAR = new Vector3(1.3f, 1.3f, 1.3f);

    public TextMeshProUGUI countText;
    public GameManager gm;

    public static List<string> TimeStamps = new List<string>();
    private string time;

    public AudioClip pickupSound;
    public AudioClip growSound;
    public AudioClip loseSound;

    private Rigidbody rb;
    private float movementX, movementY;
    private float speed;
    private int countBasic; // number of basic pickups
    private int countSpecial; // number of special pickups
    private int total;
    private bool affected;
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 12;
        countBasic = 0;
        countSpecial = 0;
        total = 0;
        affected = false;
        curTime = 0;
        setCountText();
        gm = FindObjectOfType<GameManager>();
        time = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second;
        TimeStamps.Insert(gm.getTotalRounds(), time);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void setCountText()
    {
        total = countBasic + countSpecial;
        countText.text = "Total Count: " + total.ToString();
        if(total >= NUM_PICKUPS)
        {
            gm.WinGame();
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * getSpeed());
        //rb.velocity = movement * getSpeed();
        if (changed() && curTime == 0)
        {
            curTime = Time.time; // time since app starts, in seconds
        }
        if (changed() && Time.time - curTime > DURATION)
        {
            UnDo();
            affected = false;
        }
    }

    // Undo effects of Grow pickup
    void UnDo()
    {
        transform.localScale -= SCALAR;
        speed += SPEED_CHANGE;
    }

    // triggered when the Player touches a trigger collider, called other
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") ||
            other.gameObject.CompareTag("Grow"))
        {
            other.gameObject.SetActive(false); // disable GameObject
            if (other.gameObject.CompareTag("Grow"))
            {
                ++countSpecial;
                AudioSource.PlayClipAtPoint(growSound, transform.position);
                transform.localScale += SCALAR;
                speed -= SPEED_CHANGE;
                affected = true;
            }
            else
            {
                ++countBasic;
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            setCountText();
        }
        
        if (other.gameObject.CompareTag("Enemy")){
            AudioSource.PlayClipAtPoint(loseSound, transform.position);
            gm.LoseGame();
        }
    }

    public int getBasicCount()
    {
        return countBasic;
    }

    public int getSpecialCount()
    {
        return countSpecial;
    }

    public int getTotalCount()
    {
        return total;
    }

    public float getSpeed()
    {
        return speed;
    }

    public bool changed()
    {
        return affected;
    }

    public string getTimeAt(int i)
    {
        return TimeStamps[i];
    }
}
