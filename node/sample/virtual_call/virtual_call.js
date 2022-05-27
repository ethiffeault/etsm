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