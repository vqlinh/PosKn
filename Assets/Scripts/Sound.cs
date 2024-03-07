using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public enum SoundName {
  BackGroundMusic,
  SfxShoot,
  SfxClickButton,
  SfxMenuTutorial
}

[Serializable]
public class Sound {
  public SoundName name;
  public AudioClip clip;
}
