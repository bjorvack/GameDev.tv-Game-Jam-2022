using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }

    public AudioManager AudioManager { get; private set; }
    public GameSession GameSession { get; private set; }

    void Start()
    {
        SwitchState(new PlayerMoveState(this));
        AudioManager = FindObjectOfType<AudioManager>();
        GameSession = FindObjectOfType<GameSession>();
    }
}
