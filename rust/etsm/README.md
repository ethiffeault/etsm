[![Version](https://img.shields.io/crates/v/etsm.svg)](https://crates.io/crates/etsm)
# Rust etsm
Tiny state machine for rust, see [etsm](../../../../)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- enter/exit methods
- virtual state methods
- is in
- unrestricted transitions
- no runtime allocation

# Examples

## Simple
Declare state machine
```
use etsm::*;
state_machine!(
    Foo,
    State {
        {A, Some(Foo::enter_a), Some(Foo::exit_a)},
        {B, Some(Foo::enter_b), None}
    }
);
```

Instanciate the state machine in your struct
```
struct Foo {
    state_machine: StateMachine<State>,
}

impl Foo {
    fn new() -> Foo {
        Foo {
            state_machine: StateMachine::new(),
        }
    }
}
```

Add enter/exit callbacks
```
impl Foo {
    //...
    fn enter_a(&mut self) {
        print!(" ->A ");
    }

    fn exit_a(&mut self) {
        print!(" A-> ");
    }

    fn enter_b(&mut self) {
        print!(" ->B ");
    }
}
```

Execute transitions
```
    fn run(&mut self) {
        transition!(self, state_machine, Some(State::A));
        transition!(self, state_machine, Some(State::B));
        transition!(self, state_machine, None);
    }
```

Output: " ->A  A-> ->B "

full sample [here](tests/simple.rs)

## Virtual State Methods

Declare state machine with data, you might use any type of data, view this data as static data for each state.
```
state_machine!(
    Foo,
    Data,
    State {
        // state, enter, exit, data
        {  A,     None,  None, Some(Data { run : Foo::run_a })},
        {  B,     None,  None, Some(Data { run : Foo::run_b })}
    }
);
```

Declare the statedata struct
```
struct Data {
    run: fn(&mut Foo),
}
```

Implement Foo
```
struct Foo {
    state_machine: StateMachine<State>,
}

impl Foo {
    fn new() -> Foo {
        Foo {
            state_machine: StateMachine::new(),
        }
    }

    fn run_a(&mut self) {
        print!(" A ");
    }

    fn run_b(&mut self) {
        print!(" B ");
    }

    fn run(&mut self) {
        // call run on the current state
        if let Some(statedata) = get_statedata(&self.state_machine) {
            (statedata.run)(self);
        }
    }
}
```

Call method run on Foo that forward it to the current state
```
#[test]
fn virtual_call() {
    let mut foo = Foo::new();

    transition!(&mut foo, state_machine, Some(State::A));
    foo.run();

    transition!(&mut foo, state_machine, Some(State::B));
    foo.run();

    // output: A B
}
```
full sample [here](tests/virtual_call.rs)
