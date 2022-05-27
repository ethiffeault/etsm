package com.etsm;

import static org.junit.Assert.assertTrue;
import org.junit.Test;

public class TestAb {
    private StateMachine sm;
    private State a;
    private State b;
    private String output;

    public TestAb() {
        sm = new StateMachine();
        a = new State(() -> {this.EnterA();}, () -> {this.ExitA();});
        b = new State(() -> {this.EnterB();}, null);
        output = "";
    }

    private void EnterA() {
        output += " ->A ";
    }

    private void ExitA() {
        output += " A-> ";
    }

    private void EnterB() {
        output += " ->B ";
    }

    // no exit callback for state B
    // private void ExitB()
    // {
    // output += " B-> ";
    // }

    @Test
    public void shouldAnswerWithTrue() {
        assertTrue(sm.IsIn(null));

        sm.Transition(a);
        assertTrue(sm.IsIn(a));

        sm.Transition(b);
        assertTrue(sm.IsIn(b));

        sm.Transition(null);
        assertTrue(sm.IsIn(null));

        assertTrue(output.equals(" ->A  A->  ->B "));
    }
}
