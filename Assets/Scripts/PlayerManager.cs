using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    int characterIndex;
    private void Awake()
    {
        characterIndex= PlayerPrefs.GetInt("SelectedCharacter",0);
        Instantiate(playerPrefabs[characterIndex],new Vector2(-3,0),Quaternion.identity);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
