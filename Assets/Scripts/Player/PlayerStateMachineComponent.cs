using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KevinCastejon.HierarchicalFiniteStateMachine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateMachineComponent : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    [SerializeField] GameObject WeaponObject;
    [SerializeField] bool ActiveAtStart;
    public UnityEvent OnWeaponEnable, OnWeaponDisable;

    [HideInInspector] public bool WeaponEnabled;
     
    private void Awake()
    {
        if (ActiveAtStart) EnableWeapon();
        else DisableWeapon();

        _stateMachine = AbstractHierarchicalFiniteStateMachine.CreateRootStateMachine<PlayerStateMachine>("PlayerStateMachine");
        _stateMachine.rb = GetComponent<Rigidbody2D>();
        _stateMachine.sensors = GetComponent<PlayerDetectionSensors>();
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

    public void EnableWeapon()
    {
        WeaponEnabled = true;
        WeaponObject.SetActive(true);
        OnWeaponEnable?.Invoke();
    }

    public void DisableWeapon()
    {
        WeaponEnabled = false;
        WeaponObject.SetActive(false);
        OnWeaponDisable?.Invoke();
    }

}
