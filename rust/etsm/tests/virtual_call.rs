use etsm::*;

state_machine!(
    Foo,
    Data,
    State {
        // state, enter, exit, data
        {  A,     None,  None, Some(Data { run : Foo::run_a })},
        {  B,     None,  None, Some(Data { run : Foo::run_b })}
    }
);

struct Data {
    run: fn(&mut Foo),
}

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

#[test]
fn virtual_call() {
    let mut foo = Foo::new();

    transition!(&mut foo, state_machine, Some(State::A));
    foo.run();

    transition!(&mut foo, state_machine, Some(State::B));
    foo.run();

    // output: A B
}
