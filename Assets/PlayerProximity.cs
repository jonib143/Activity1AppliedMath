using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerProximity : MonoBehaviour
{
    public float speed = 5f;

    public Transform noGoZone;

    public Transform finishZone;
    public GameObject winUI;
    public float noGoZoneTimer;
    
    private Vector3 noGoZonePos;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noGoZonePos = noGoZone.position;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        Vector2 move = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                move.y += 1;
            if (Keyboard.current.aKey.isPressed)
                move.x -= 1;
            if (Keyboard.current.sKey.isPressed)
                move.y -= 1;
            if (Keyboard.current.dKey.isPressed)
                move.x += 1;
        }


        Vector3 dir = move.normalized;
        transform.position += dir * speed * Time.deltaTime;

        // finish zone checker
        Vector3 finish = finishZone.position - transform.position;
        if (finish.magnitude < 2f)
        {
            winUI.SetActive(true);
            enabled  = false;
            return;
        }
        
        // no go zone checher
        Vector3 nogo = noGoZone.position - transform.position;
        float nogoMag =  nogo.magnitude;
        
        if (nogoMag < 1f || noGoZoneTimer > 2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (nogoMag < 4f)
        {
            noGoZoneTimer += Time.deltaTime;
            noGoZone.GetComponent<Renderer>().material.color = Color.red;
            noGoZone.position = noGoZonePos + new Vector3(Mathf.Sin(Time.time * 30f) * 0.1f, 0, 0);
        }
        else
        {
            noGoZoneTimer = 0f;
            noGoZone.GetComponent<Renderer>().material.color = Color.white;
            noGoZonePos = noGoZone.position;
        }

    }
}
