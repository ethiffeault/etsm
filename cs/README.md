[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/etsm.svg)](https://www.nuget.org/packages/etsm)

# c# etsm
Tiny state machine for c#, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

Edit your vcproj and add:
```
  <ItemGroup>
    <PackageReference Include="etsm" Version="x.y.z" />
  </ItemGroup>
```  
latest version: [![NuGet](https://img.shields.io/nuget/v/etsm.svg)](https://www.nuget.org/packages/etsm)

Or

Add this file directly into your project: [etsm](https://github.com/ethiffeault/etsm/blob/main/cs/etsm/etsm.cs)

# Example

## Simple
Declare state machine and state in your class
```cs
    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;
    ...
```

Create them in the constructor and bind enter/exit
```cs
        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(EnterA, ExitA);
            stateB = new State(EnterB, null);
        }
```

Make your callback methods
```cs
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
```cs
        public void Run()
        {
            stateMachine.Transition(stateA);
            stateMachine.Transition(stateB);
            stateMachine.Transition(null);
        }
```

Output: " ->A  A-> ->B "\
Sample: [ab](https://github.com/ethiffeault/etsm/tree/main/cs/sample/ab)

## Virtual State Methods

```cs
    // Define your own state and add Run Action.
    public class State : Etsm.State
    {
        public Action Run { get; private set; }

        public State(Action enter, Action exit, Action run = null)
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
Sample: [virtual_call](https://github.com/ethiffeault/etsm/tree/main/cs/sample/virtual_call)
