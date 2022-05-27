[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![npm version](https://badge.fury.io/js/etsm.svg)](https://badge.fury.io/js/etsm)

# node etsm
Tiny state machine for node, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

edit package.json
```
  "dependencies": {
    "etsm": "*.*.*"
  }  
```

install packages
```
npm install
```

in your js file:
```node
var etsm = require('etsm');
//...
```

Or simply drop this file into your codebase: [etsm.js](https://github.com/ethiffeault/etsm/blob/main/node/etsm/etsm.js)

# Example

## Simple

```node
var etsm = require('etsm');

class Foo {
    constructor() {
        this.sm = new etsm.StateMachine(this);
        this.a = new etsm.State(this.EnterA, this.ExitA);
        this.b = new etsm.State(this.EnterB, null);
    }

    EnterA(){
        process.stdout.write(' ->A ');
    }

    ExitA(){
        process.stdout.write(' A-> ');
    }

    EnterB(){
        process.stdout.write(' ->B ');
    }

    // don't implement Exit for B state
    // ExitB(){
    //     process.stdout.write(' B-> ');
    //     assert(this.sm.Transition(this.a) == false);
    // }

    Test() {
        this.sm.Transition(this.a);
        this.sm.Transition(this.b);
        this.sm.Transition(null);
    }    
}

foo = new Foo();
foo.Test();
```

Output: " ->A  A-> ->B "\
Sample: [ab](https://github.com/ethiffeault/etsm/blob/main/node/sample/ab/ab.js)

## Virtual State Methods

```node
var etsm = require('etsm');

class FooState extends etsm.State {
    constructor(enter, exit, tick) {
        super(enter, exit);
        this.Tick = tick;
    }
}

class Foo {
    constructor() {
        this.sm = new etsm.StateMachine(this);
        this.a = new FooState(null, null, this.TickA);
        this.b = new FooState(null, null, this.TickB);
    }

    TickA(){
        process.stdout.write(' A ');
    }

    TickB(){
        process.stdout.write(' B ');
    }

    Tick(){
        if ( this.sm.current != null && this.sm.current.Tick != null)
            this.sm.current.Tick.apply(this);
    }

    Test() {
        this.Tick();

        this.sm.Transition(this.a);
        this.Tick();

        this.sm.Transition(this.b);
        this.Tick();

        this.sm.Transition(null);
        this.Tick();
    }    
}

foo = new Foo();
foo.Test();
```

Output: " A   B "\
Sample: [virtual_call](https://github.com/ethiffeault/etsm/blob/main/node/sample/virtual_call/virtual_call.js)
