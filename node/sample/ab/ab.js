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


