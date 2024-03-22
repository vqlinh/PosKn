using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{
    public const string enemy = "Enemy";
    public const string player = "Player";

    public const string animMove = "isMove";
    public const string animDie = "isDie";
    public const string animDamaged = "isDamaged";
    public const string animIdle = "isIdle";
    public const string animNormalAttack = "isNormalAttack";
    public const string animSkillAttack = "isSkillAttack";
    public const string animDestroyShield = "ShieldDestroy";
    public const string meleeMove = "isMove";
    public const string meleeAttack = "isAttack";
    public const string meleeIdle = "isIdle";
    public const string meleeDamaged = "isDamaged";
    public const string rangedAttack = "isAttack";
    public const string rangedIdle = "isIdle";
    public const string rangedDamaged = "isDamaged";

    public const string chief = "Chief";
    public const string isLoading = "isLoading";
    public const string isOpen = "isOpen";
    public const string homeScene = "HomeScene";
    public const string level1 = "Level1";
    public const string level2 = "Level2";
    public const string level3 = "Level3";
    public const string level4 = "Level4";
    public const string level5 = "Level5";
    public const string mainMenu = "MainMenu ";

    public const string coin = "Coin";

    public const string Scene_StartGame = "StartGame";
    public const string Tag_Player = "Player";

    [Header("Anim")]
    public const string Anim_PlayerJump = "Jump";
    public const string Anim_PlayerIdle = "Idle";
    public const string Anim_PlayerWalk = "Walk";

    public const string Anim_BrickIdle = "IdleBrick";
    public const string Anim_BrickBroken = "BrokenBrick";

    [Header("Prefs")]
    public const string PrefsKey_PlayerHealth = "PlayerHealth";
    public const string PrefsKey_MusicVolume = "MusicVolume";
    public const string PrefsKey_SfxVolume = "SfxVolume";

    [Header("Scene")]
    public const string Scene_MainMenu = "MainMenu";
}
