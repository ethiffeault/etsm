use etsm::*;

state_machine!(
    Foo,
    State {
        {A, Some(Foo::enter_a), Some(Foo::exit_a)},
        {B, Some(Foo::enter_b), None}
    }
);

struct Foo {
    state_machine: StateMachine<State>,
}

impl Foo {
    fn new() -> Foo {
        Foo {
            state_machine: StateMachine::new(),
        }
    }

    fn run(&mut self) {
        transition!(self, state_machine, Some(State::A));
        transition!(self, state_machine, Some(State::B));
        transition!(self, state_machine, None);
    }

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

#[test]
fn simple() {
    let mut foo = Foo::new();
    foo.run();
}
