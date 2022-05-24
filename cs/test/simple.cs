using Microsoft.VisualStudio.TestTools.UnitTesting;
using Etsm;

namespace simple
{
    public class Foo
    {
        private StateMachine<State> stateMachine;
        private State stateA;
        private State stateB;

        public Foo()
        {
            stateMachine = new StateMachine<State>();
            stateA = new State(EnterA, ExitA);
            stateB = new State(EnterB, null);
        }

        public void Run()
        {
            stateMachine.Transition(stateA);
            stateMachine.Transition(stateB);
            stateMachine.Transition(null);
        }

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
    }

    [TestClass]
    public class simple
    {
        [TestMethod]
        public void Test()
        {
            Foo foo = new Foo();
            // Output: " ->A  A->  ->B "
            foo.Run();
        }
    }
}