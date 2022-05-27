package com.sample.ab;

import com.etsm.*;

public class Ab 
{
    private StateMachine sm;
    private State a;
    private State b;

    public Ab() {
        sm = new StateMachine();
        a = new State(() -> {this.EnterA();}, () -> {this.ExitA();});
        b = new State(() -> {this.EnterB();}, null);
    }

    private void EnterA() {
        System.out.print(" ->A ");
    }

    private void ExitA() {
        System.out.print(" A-> ");
    }

    private void EnterB() {
        System.out.print(" ->B ");
    }

    private void Test(){
        sm.Transition(a);
        sm.Transition(b);
        sm.Transition(null);
    }

    // no exit callback for state B
    // private void ExitB()
    // {
    //     System.out.print(" B-> ");
    // }

    // Output: " ->A  A->  ->B "
    public static void main( String[] args )
    {
        Ab ab = new Ab();
        ab.Test();
    }
}
