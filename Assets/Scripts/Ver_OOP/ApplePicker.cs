using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    
    // Start is called before the first frame update
    void Start()
    {
        int index;
        
        basketList = new List<GameObject>();
        
        for ( index = 0; index < numBaskets; index++ )
        {    
            // NOTE: lowercase t = temporary
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * index);
            tBasketGO.transform.position = pos;
            
            basketList.Add( tBasketGO );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AppleMissed()
    {
        GameObject[] appleArray=GameObject.FindGameObjectsWithTag( "Apple" );
        
        foreach ( GameObject tempGO in appleArray )
        {
            Destroy( tempGO );
        }
        
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[ basketIndex ];
        basketList.RemoveAt( basketIndex );
        Destroy( basketGO );
        
        if ( basketList.Count == 0 )
        {
            SceneManager.LoadScene( "_Scene_Start" );
        }
    }
}
