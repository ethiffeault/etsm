var etsm = require('./../etsm');
var assert = require('assert');

class Foo {
    constructor() {
        this.sm = new etsm.StateMachine(this);
        this.a = new etsm.State(this.EnterA, this.ExitA);
        this.b = new etsm.State(this.EnterB, null);
        this.output = '';
    }

    EnterA(){
        this.output += ' ->A ';
        assert(this.sm.Transition(this.a) == false);
    }

    ExitA(){
        this.output += ' A-> ';
        assert(this.sm.Transition(this.a) == false);
    }

    EnterB(){
        this.output += ' ->B ';
        assert(this.sm.Transition(this.a) == false);
    }

    // don't implement Exit for B state
    // ExitB(){
    //     this.output += ' B-> ';
    //     assert(this.sm.Transition(this.a) == false);
    // }

    Test() {
        assert(this.sm.IsIn(null));

        this.sm.Transition(this.a);
        assert(this.sm.IsIn(this.a));

        this.sm.Transition(this.b);
        assert(this.sm.IsIn(this.b));

        this.sm.Transition(null);
        assert(this.sm.IsIn(null));

        assert(this.output == ' ->A  A->  ->B ');
    }    
}

foo = new Foo();
foo.Test();


