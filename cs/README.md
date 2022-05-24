# c# etsm
Tiny state machine for c#, see [etsm](../../../../)

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

full sample [here](test/simple.cs)

## Virtual State Methods

Declare state machine with data, you might use any type of data, view this data as static data for each state.
```
```

Declare the statedata struct
```
```

Implement Foo
```
```

Call method run on Foo that forward it to the current state
```
```
full sample [here](tests/virtual_call.rs)
