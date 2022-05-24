namespace etsm
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public class State : IState
    {
        private Action? enter;
        private Action? exit;

        public State(Action? enter, Action? exit)
        {
            this.enter = enter;
            this.exit = exit;
        }

        public void Enter()
        {
            enter?.Invoke();
        }

        public void Exit()
        {
            exit?.Invoke();
        }
    }

    public class StateMachine<STATE> where STATE : class, IState
    {
        public STATE? CurrentState { get; private set; }

        public void Transition(STATE? state)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = state;

            if (CurrentState != null)
                CurrentState.Enter();
        }

        public bool IsIn(STATE? s)
        {
            return CurrentState == s;
        }
    }
}