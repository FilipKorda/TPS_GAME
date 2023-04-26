using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator startGame;

    private void Start()
    {
        startGame.Play("ShowHpBarAnimation");
    }
}