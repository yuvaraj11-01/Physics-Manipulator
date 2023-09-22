using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.HierarchicalFiniteStateMachine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateMachineComponent : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = AbstractHierarchicalFiniteStateMachine.CreateRootStateMachine<PlayerStateMachine>("PlayerStateMachine");
    }
    private void Start()
    {
        _stateMachine.OnEnter();
    }
    private void Update()
    {
        _stateMachine.OnUpdate();
    }
    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }
}
