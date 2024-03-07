using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
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
