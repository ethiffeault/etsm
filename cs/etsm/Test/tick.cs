using Microsoft.VisualStudio.TestTools.UnitTesting;
using etsm;
using System;

namespace tick
{
    public class State : etsm.State
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
        public StateMachine<State> StateMachine { get; private set; }
        public State StateA { get; private set; }
        public State StateB { get; private set; }

        public int tickStateA = 0;
        public int tickStateB = 0;

        public Foo()
        {
            StateMachine = new StateMachine<State>();
            StateA = new State(null, null, TickA);
            StateB = new State(null, null, TickB);
        }

        private void TickA()
        {
            ++tickStateA;
        }

        private void TickB()
        {
            ++tickStateB;
        }
    }

    [TestClass]
    public class tick
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();

            Assert.IsTrue(foo.StateMachine.IsIn(null)); 
            Assert.AreEqual(0, foo.tickStateA);
            Assert.AreEqual(0, foo.tickStateB);

            foo.StateMachine.Transition(foo.StateA);
            Assert.IsTrue(foo.StateMachine.IsIn(foo.StateA));
            foo.StateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, foo.tickStateA);
            Assert.AreEqual(0, foo.tickStateB);

            foo.StateMachine.Transition(foo.StateB);
            Assert.IsTrue(foo.StateMachine.IsIn(foo.StateB));
            foo.StateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, foo.tickStateA);
            Assert.AreEqual(1, foo.tickStateB);

            foo.StateMachine.Transition(null);
            Assert.IsTrue(foo.StateMachine.IsIn(null));
            foo.StateMachine.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, foo.tickStateA);
            Assert.AreEqual(1, foo.tickStateB);
        }
    }
}
