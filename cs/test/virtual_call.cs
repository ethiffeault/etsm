using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;
using System;

namespace virtual_call
{
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

    [TestClass]
    public class virtual_call
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();
            foo.Test();
        }
    }
}
