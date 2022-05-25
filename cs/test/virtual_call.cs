using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;
using System;

namespace virtual_call
{
    public class State : Etsm.State
    {
        public Action Tick { get; private set; }

        public State(Action enter, Action exit, Action tick = null)
            : base(enter, exit)
        {
            Tick = tick;
        }
    }

    public class Foo
    {
        private StateMachine<State> sm;
        private State a;
        private State b;

        private int tickA = 0;
        private int tickB = 0;

        public Foo()
        {
            sm = new StateMachine<State>();
            a = new State(null, null, TickA);
            b = new State(null, null, TickB);
        }

        private void TickA()
        {
            ++tickA;
        }

        private void TickB()
        {
            ++tickB;
        }

        public void Test()
        {
            Assert.IsTrue(sm.IsIn(null));
            Assert.AreEqual(0, tickA);
            Assert.AreEqual(0, tickB);

            sm.Transition(a);
            Assert.IsTrue(sm.IsIn(a));
            sm.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickA);
            Assert.AreEqual(0, tickB);

            sm.Transition(b);
            Assert.IsTrue(sm.IsIn(b));
            sm.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickA);
            Assert.AreEqual(1, tickB);

            sm.Transition(null);
            Assert.IsTrue(sm.IsIn(null));
            sm.CurrentState?.Tick?.Invoke();
            Assert.AreEqual(1, tickA);
            Assert.AreEqual(1, tickB);
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
