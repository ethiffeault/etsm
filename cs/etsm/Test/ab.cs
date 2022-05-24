using Microsoft.VisualStudio.TestTools.UnitTesting;
using etsm;

namespace ab
{
    public class Foo
    {
        public StateMachine<State> StateMachine { get; private set; }
        public State StateA { get; private set; }
        public State StateB { get; private set; }

        public int enterStateA = 0;
        public int exitStateA = 0;
        public int enterStateB = 0;
        public int exitStateB = 0;

        public Foo()
        {
            StateMachine = new StateMachine<State>();
            StateA = new State(EnterA, ExitA);
            StateB = new State(EnterB, ExitB);
        }

        // Simple simulation that make transition using this pattern:
        //    None -> A -> B -> None
        public void Simulation()
        {
            if (StateMachine.IsIn(null))
                StateMachine.Transition(StateA);
            else if (StateMachine.IsIn(StateA))
                StateMachine.Transition(StateB);
            else if (StateMachine.IsIn(StateB))
                StateMachine.Transition(null);
        }

        void EnterA()
        {
            enterStateA++;
        }

        void ExitA()
        {
            exitStateA++;
        }

        void EnterB()
        {
            enterStateB++;
        }

        void ExitB()
        {
            exitStateB++;
        }
    }

    [TestClass]
    public class ab
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();

            for (int i = 1; i < 3; i++)
            {
                Assert.IsTrue(foo.StateMachine.IsIn(null));

                foo.Simulation();
                Assert.IsTrue(foo.StateMachine.IsIn(foo.StateA));

                foo.Simulation();
                Assert.IsTrue(foo.StateMachine.IsIn(foo.StateB));

                foo.Simulation();
                Assert.IsTrue(foo.StateMachine.IsIn(null));

                Assert.AreEqual(i, foo.enterStateA);
                Assert.AreEqual(i, foo.exitStateA);
                Assert.AreEqual(i, foo.enterStateB);
                Assert.AreEqual(i, foo.enterStateB);
            }
        }
    }
}