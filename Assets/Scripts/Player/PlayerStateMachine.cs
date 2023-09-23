using KevinCastejon.HierarchicalFiniteStateMachine;
using UnityEngine;

public class PlayerStateMachine : AbstractHierarchicalFiniteStateMachine
{
    public enum PlayerState
    {
        IDLE,
        MOVE,
        JUMP,
        FALL,
        DEAD
    }


    // properties
    public Rigidbody2D rb;
    public InputController inputs;
    public PlayerDetectionSensors sensors;
    float moveSpeed = 10;

    public PlayerStateMachine()
    {
        Init(PlayerState.IDLE,
            Create<IdleState, PlayerState>(PlayerState.IDLE, this),
            Create<MoveState, PlayerState>(PlayerState.MOVE, this),
            Create<JumpState, PlayerState>(PlayerState.JUMP, this),
            Create<FallState, PlayerState>(PlayerState.FALL, this),
            Create<DeadState, PlayerState>(PlayerState.DEAD, this)
        );
    }
    public override void OnStateMachineEntry()
    {
        inputs = new InputController();
        inputs.Player.Enable();
    }
    public override void OnStateMachineExit()
    {
    }
    public class IdleState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log("[Player] Idle State");
            _psm.inputs.Player.Jump.performed += Jump_performed;
        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_psm.sensors.IsGrounded())
                TransitionToState(PlayerState.JUMP);
        }

        public override void OnUpdate()
        {
            if (!_psm.sensors.IsGrounded()) TransitionToState(PlayerState.FALL);

            var readValue = _psm.inputs.Player.Move.ReadValue<float>();
            if (readValue != 0)
            {
                TransitionToState(PlayerState.MOVE);
            }
            var current = _psm.rb.velocity;
            _psm.rb.velocity = new Vector2(0, current.y);
        }
        public override void OnFixedUpdate()
        {
        }
        public override void OnExit()
        {
            _psm.inputs.Player.Jump.performed -= Jump_performed;
        }
    }
    public class MoveState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log("[Player] Move State");
            _psm.inputs.Player.Jump.performed += Jump_performed;

        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if(_psm.sensors.IsGrounded())
                TransitionToState(PlayerState.JUMP);
        }

        public override void OnUpdate()
        {
            if(!_psm.sensors.IsGrounded()) TransitionToState(PlayerState.FALL);

            var readValue = _psm.inputs.Player.Move.ReadValue<float>();
            if (readValue == 0)
            {
                TransitionToState(PlayerState.IDLE);
            }
        }
        public override void OnFixedUpdate()
        {
            var readValue = _psm.inputs.Player.Move.ReadValue<float>();
            var current = _psm.rb.velocity;
            _psm.rb.velocity = new Vector2(readValue * _psm.moveSpeed, current.y);
        }
        public override void OnExit()
        {
            _psm.inputs.Player.Jump.performed -= Jump_performed;
        }
    }
    public class JumpState : AbstractState
    {
        PlayerStateMachine _psm;
        float JumpDuration = .25f;

        float lastjump;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log("[Player] Jump State");
            _psm.rb.AddForce(Vector2.up * 1000);
            lastjump = Time.time + JumpDuration;
        }
        public override void OnUpdate()
        {
            if(_psm.sensors.IsCeiled()) TransitionToState(PlayerState.FALL);
            //if(_psm.sensors.IsGrounded()) TransitionToState(PlayerState.IDLE);
            if (Time.time >= lastjump)
            {
                TransitionToState(PlayerState.FALL);
            }

        }
        public override void OnFixedUpdate()
        {
            //var readValue = _psm.inputs.Player.Move.ReadValue<float>();
            //var current = _psm.rb.velocity;
            //_psm.rb.velocity = new Vector2(readValue * _psm.moveSpeed, current.y);
        }
        public override void OnExit()
        {
        }
    }
    public class FallState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log("[Player] Fall State");

        }
        public override void OnUpdate()
        {
            if (_psm.sensors.IsGrounded()) TransitionToState(PlayerState.IDLE);
        }
        public override void OnFixedUpdate()
        {
            var readValue = _psm.inputs.Player.Move.ReadValue<float>();
            var current = _psm.rb.velocity;
            _psm.rb.velocity = new Vector2(readValue * _psm.moveSpeed, current.y);
        }
        public override void OnExit()
        {
        }
    }
    public class DeadState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log("[Player] Dead State");

        }
        public override void OnUpdate()
        {
        }
        public override void OnFixedUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
}
