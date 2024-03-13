using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag(Const.player).GetComponent<Player>();
    }

    public void Skill1()
    {
        player.SkillAttackState();
    }
    public void Skill3()
    {
        player.Skillhealing();
    }
    public void Skill2()
    {
        player.SkillShield();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
