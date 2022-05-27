[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# java etsm
Tiny state machine for java, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

Or simply drop this file into your codebase: [etsm.java]()

# Example

## Simple

```java
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

    public static void main( String[] args )
    {
        Ab ab = new Ab();
        ab.Test();
    }
}
```

Output: " ->A  A-> ->B "\
Sample: [ab]()

## Virtual State Methods

```java
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

    public static void main( String[] args )
    {
        VirtualCall virtualCall = new VirtualCall();
        virtualCall.Test();
    }
}
```

Output: " A   B "\
Sample: [virtual_call]()
