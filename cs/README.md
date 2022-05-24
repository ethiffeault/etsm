# c# etsm
Tiny state machine for c#, see [etsm](https://github.com/ethiffeault/etsm)

# Example

## Simple
Declare state machine and state in your class
```
    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;
    ...
```

Create them in the constructor and bind enter/exit
```
        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(EnterA, ExitA);
            stateB = new State(EnterB, null);
        }
```

Make your callback methods
```
        void EnterA()
        {
            System.Console.Write(" ->A ");
        }

        void ExitA()
        {
            System.Console.Write(" A-> ");
        }

        void EnterB()
        {
            System.Console.Write(" ->B ");
        }
```

Execute transitions
```
        public void Run()
        {
            stateMachine.Transition(stateA);
            stateMachine.Transition(stateB);
            stateMachine.Transition(null);
        }
```

Output: " ->A  A-> ->B "

full sample [here](https://github.com/ethiffeault/etsm/blob/main/cs/test/simple.cs)

## Virtual State Methods

```
    // Define your own state and add Run Action.
    public class State : Etsm.State
    {
        public Action? Run { get; private set; }

        public State(Action? enter, Action? exit, Action? run = null)
            : base(enter, exit)
        {
            Run = run;
        }
    }

    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;

        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(null, null, RunA);
            stateB = new State(null, null, RunB);
        }

        private void RunA()
        {
            Console.Write(" A ");
        }

        private void RunB()
        {
            Console.Write(" B ");
        }

        // call run on the current state
        private void Run()
        {
            stateMachine.CurrentState?.Run?.Invoke();
        }

        public void Test()
        {
            stateMachine.Transition(stateA);
            Run();
            stateMachine.Transition(stateB);
            Run();
        }
    }
```
Output: " A   B "\
full sample [here](https://github.com/ethiffeault/etsm/blob/main/cs/test/virtual_call.cs)
