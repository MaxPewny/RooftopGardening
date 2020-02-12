using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelDisplay : MonoBehaviour
{

    public AudioSource OpenMenuAudio;

    private void Awake()
    {
        OpenMenuAudio.Play();
    }

    public Text LevelText;
    public Image XpImage;

    private void Update()
    {
        LevelText.text = ((int)GameplayController.Instance.PlayerLevel).ToString();
        XpImage.fillAmount = GameplayController.Instance.PlayerXp / GameplayController.Instance.MaxPlayerXp;
    }
}
