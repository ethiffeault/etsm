var etsm = require('./../etsm');
var assert = require('assert');

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
        this.output = '';
    }

    TickA(){
        this.output += ' A ';
    }

    TickB(){
        this.output += ' B ';
    }

    Tick(){
        if ( this.sm.current != null && this.sm.current.Tick != null)
            this.sm.current.Tick.apply(this);
    }

    Test() {
        assert(this.sm.IsIn(null));

        this.sm.Transition(this.a);
        assert(this.sm.IsIn(this.a));
        this.Tick();

        this.sm.Transition(this.b);
        assert(this.sm.IsIn(this.b));
        this.Tick();

        this.sm.Transition(null);
        assert(this.sm.IsIn(null));

        assert(this.output == ' A  B ');
    }    
}

foo = new Foo();
foo.Test();