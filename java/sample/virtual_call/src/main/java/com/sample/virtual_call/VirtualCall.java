package com.sample.virtual_call;

import com.etsm.*;

public class VirtualCall 
{
    private StateMachine sm;
    private State a;
    private State b;

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

    public VirtualCall() {
        sm = new StateMachine();
        a = new State(null, null, () -> {this.TickA();});
        b = new State(null, null, () -> {this.TickB();});
    }

    private void TickA() {
        System.out.print(" A ");

    }

    private void TickB() {
        System.out.print(" B ");
    }

    private void Tick() {
        if (sm.GetCurrent() instanceof State)
            ((State) sm.GetCurrent()).Tick();
    }

    public void Test() {
        Tick();

        sm.Transition(a);
        Tick();

        sm.Transition(b);
        Tick();

        sm.Transition(null);
        Tick();
    }

    // Output: " A  B "
    public static void main( String[] args )
    {
        VirtualCall virtualCall = new VirtualCall();
        virtualCall.Test();
    }
}
