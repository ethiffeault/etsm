package com.etsm;

import static org.junit.Assert.assertTrue;
import org.junit.Test;

public class TestVirtualCall {
    private StateMachine sm;
    private State a;
    private State b;
    private String output;

    // redefine you own state and add Tick
    public class State extends com.etsm.State {
        public State(IMethod enter, IMethod exit, IMethod tick) {
            super(enter, exit);
            this.tick = tick;
        }

        public void Tick() {
            if (tick != null)
                tick.Call();
        }

        private IMethod tick;
    }

    public TestVirtualCall() {
        sm = new StateMachine();
        a = new State(null, null, () -> {this.TickA();});
        b = new State(null, null, () -> {this.TickB();});
        output = "";
    }

    private void TickA() {
        output += " A ";
    }

    private void TickB() {
        output += " B ";
    }

    private void Tick() {
        if (sm.GetCurrent() instanceof State)
            ((State) sm.GetCurrent()).Tick();
    }

    @Test
    public void shouldAnswerWithTrue() {
        assertTrue(sm.IsIn(null));

        sm.Transition(a);
        assertTrue(sm.IsIn(a));
        Tick();

        sm.Transition(b);
        assertTrue(sm.IsIn(b));
        Tick();

        sm.Transition(null);
        assertTrue(sm.IsIn(null));

        assertTrue(output.equals(" A  B "));
    }
}
