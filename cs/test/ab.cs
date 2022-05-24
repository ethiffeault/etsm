using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;

namespace ab
{
    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;

        private int enterStateA = 0;
        private int exitStateA = 0;
        private int enterStateB = 0;
        private int exitStateB = 0;

        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(EnterA, ExitA);
            stateB = new State(EnterB, ExitB);
        }

        // Simple simulation that make transition using this pattern:
        //    None -> A -> B -> None
        public void Simulation()
        {
            if (stateMachine.IsIn(null))
                stateMachine.Transition(stateA);
            else if (stateMachine.IsIn(stateA))
                stateMachine.Transition(stateB);
            else if (stateMachine.IsIn(stateB))
                stateMachine.Transition(null);
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

        public void Test()
        {
            for (int i = 1; i < 3; i++)
            {
                Assert.IsTrue(stateMachine.IsIn(null));

                Simulation();
                Assert.IsTrue(stateMachine.IsIn(stateA));

                Simulation();
                Assert.IsTrue(stateMachine.IsIn(stateB));

                Simulation();
                Assert.IsTrue(stateMachine.IsIn(null));

                Assert.AreEqual(i, enterStateA);
                Assert.AreEqual(i, exitStateA);
                Assert.AreEqual(i, enterStateB);
                Assert.AreEqual(i, exitStateB);
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