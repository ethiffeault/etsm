using Etsm;

// Output: " A  B "
Foo foo = new Foo();
foo.Test();

// Define your own state and add Tick Action.
public class State : Etsm.State
{
    public Action? Tick { get; private set; }

    public State(Action? enter, Action? exit, Action? run = null)
        : base(enter, exit)
    {
        Tick = run;
    }
}

public class Foo
{
    private StateMachine<State> sm;
    private State a;
    private State b;

    public Foo()
    {
        sm = new StateMachine<State>();
        a = new State(null, null, TickA);
        b = new State(null, null, TickB);
    }

    private void TickA()
    {
        Console.Write(" A ");
    }

    private void TickB()
    {
        Console.Write(" B ");
    }

    // call run on the current state
    private void Tick()
    {
        sm.CurrentState?.Tick?.Invoke();
    }

    public void Test()
    {
        sm.Transition(a);
        Tick();
        sm.Transition(b);
        Tick();
    }
}