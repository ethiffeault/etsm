using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;
using System;

namespace tick
{
    public class State : Etsm.State
    {
        public Action? Tick { get; private set; }

        public State(Action? enter, Action? exit, Action? tick = null)
            : base(enter, exit)
        {
            Tick = tick;
        }
    }

    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;

        private int tickStateA = 0;
        private int tickStateB = 0;

        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(null, null, TickA);
            stateB = new State(null, null, TickB);
        }

        private void TickA()
        {
            ++tickStateA;
        }

        private void TickB()
        {
            ++tickStateB;
        }

        public void Test()
        {
            Assert.IsTrue(stateMachine.IsIn(null));
            Assert.AreEqual(0, tickStateA);
            Assert.AreEqual(0, tickStateB);

            stateMachine.Transition(stateA);
            Assert.IsTrue(stateMachine.IsIn(stateA));
            stateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickStateA);
            Assert.AreEqual(0, tickStateB);

            stateMachine.Transition(stateB);
            Assert.IsTrue(stateMachine.IsIn(stateB));
            stateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickStateA);
            Assert.AreEqual(1, tickStateB);

            stateMachine.Transition(null);
            Assert.IsTrue(stateMachine.IsIn(null));
            stateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickStateA);
            Assert.AreEqual(1, tickStateB);
        }
    }

    [TestClass]
    public class tick
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();
            foo.Test();
        }
    }
}
