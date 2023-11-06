using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeScript : MonoBehaviour
{
    [Header("Inscribed")] // can't change attributes while game running
    
    public ApplePicker applePicker;
    
    public GameObject applePrefab; // apple prefab
    public GameObject treePrefab;
    
    public float speed = 15f; // speed of tree
    // NOTE: public makes it accessible in UNITY inspector
    
    public float leftAndRightEdge = 10f; // distance before turning
    
    public float changeDirChance = 0.02f; // chance for changing direction
    
    public float appleDropDelay = 2f; // Delay till next apple drops

    // Start is called before the first frame update
    void Start()
    {
        Invoke( "DropApple", appleDropDelay );
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        // NOTE: Use time.deltaTime so game logic is reliant on time and not 
        //       frames, as different computers run at dfferent speeds
        transform.position = pos;
        
        if ( pos.x < -leftAndRightEdge )
        {
            speed = Mathf.Abs( speed );
        }
        else if ( pos.x > leftAndRightEdge )
        {
            speed = -Mathf.Abs( speed );
        }
    }
    
    private void FixedUpdate()
    {
        if ( Random.value < changeDirChance )
        {
            speed *= -1;
        }
    }
    
    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>( applePrefab );
        apple.transform.position = transform.position;
        Invoke( "DropApple", appleDropDelay );
    }
}
