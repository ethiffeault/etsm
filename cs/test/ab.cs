using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;

namespace ab
{
    public class Foo
    {
        private StateMachine<State> sm;
        private State a;
        private State b;

        private int enterA = 0;
        private int exitA = 0;
        private int enterB = 0;
        private int exitB = 0;

        public Foo()
        {
            sm = new StateMachine<State>();
            a = new State(EnterA, ExitA);
            b = new State(EnterB, ExitB);
        }

        // Simple simulation that make transition using this pattern:
        //    None -> A -> B -> None
        public void Simulation()
        {
            if (sm.IsIn(null))
                sm.Transition(a);
            else if (sm.IsIn(a))
                sm.Transition(b);
            else if (sm.IsIn(b))
                sm.Transition(null);
        }

        void EnterA()
        {
            enterA++;
            Assert.IsFalse(sm.Transition(a));
        }

        void ExitA()
        {
            exitA++;
            Assert.IsFalse(sm.Transition(b));
        }

        void EnterB()
        {
            enterB++;
            Assert.IsFalse(sm.Transition(a));
        }

        void ExitB()
        {
            exitB++;
            Assert.IsFalse(sm.Transition(a));
        }

        public void Test()
        {
            for (int i = 1; i < 3; i++)
            {
                Assert.IsTrue(sm.IsIn(null));

                Simulation();
                Assert.IsTrue(sm.IsIn(a));

                Simulation();
                Assert.IsTrue(sm.IsIn(b));

                Simulation();
                Assert.IsTrue(sm.IsIn(null));

                Assert.AreEqual(i, enterA);
                Assert.AreEqual(i, exitA);
                Assert.AreEqual(i, enterB);
                Assert.AreEqual(i, exitB);
            }
        }
    }

    [TestClass]
    public class ab
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();
            foo.Test();
        }
    }
}