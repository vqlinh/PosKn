using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShield : MonoBehaviour
{
    private Animator anm;
    // Start is called before the first frame update
    void Start()
    {
        anm=GetComponent<Animator>();
    }
    public void ShieldDestroy()
    {
        anm.SetBool(Const.animDestroyShield,true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
