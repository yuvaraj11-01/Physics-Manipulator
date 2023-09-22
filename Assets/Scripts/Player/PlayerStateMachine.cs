using KevinCastejon.HierarchicalFiniteStateMachine;
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
    public class MoveState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
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
    public class JumpState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
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
    public class FallState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
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
    public class DeadState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
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
