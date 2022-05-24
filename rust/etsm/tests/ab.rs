use etsm::*;

state_machine!(
    Foo,
    State {
        {A, Some(Foo::enter_a), Some(Foo::exit_a)},
        {B, Some(Foo::enter_b), Some(Foo::exit_b)}
    }
);

struct Foo {
    state_machine: StateMachine<State>,
    state_a_enter: i32,
    state_a_exit: i32,
    state_b_enter: i32,
    state_b_exit: i32,
}

impl Foo {
    fn new() -> Foo {
        Foo {
            state_machine: StateMachine::new(),
            state_a_enter: 0,
            state_a_exit: 0,
            state_b_enter: 0,
            state_b_exit: 0,
        }
    }

    // Simple simulation that make transition using this pattern:
    //    None -> A -> B -> None
    fn simulation(&mut self) {
        match &self.state_machine.current_state {
            None => {
                transition!(self, state_machine, Some(State::A));
            }
            Some(s) => match s {
                State::A => {
                    transition!(self, state_machine, Some(State::B));
                }
                State::B => {
                    transition!(self, state_machine, None);
                }
            },
        }
    }

    fn enter_a(&mut self) {
        self.state_a_enter += 1;
    }

    fn exit_a(&mut self) {
        self.state_a_exit += 1;
    }

    fn enter_b(&mut self) {
        self.state_b_enter += 1;
    }

    fn exit_b(&mut self) {
        self.state_b_exit += 1;
    }
}

#[test]
fn ab() {
    let mut foo = Foo::new();

    for i in 1..3 {
        assert!(foo.state_machine.is_in(None));
        foo.simulation();
        assert!(foo.state_machine.is_in(Some(State::A)));
        foo.simulation();
        assert!(foo.state_machine.is_in(Some(State::B)));
        foo.simulation();
        assert!(foo.state_machine.is_in(None));
        assert_eq!(foo.state_a_enter, i);
        assert_eq!(foo.state_a_exit, i);
        assert_eq!(foo.state_b_enter, i);
        assert_eq!(foo.state_b_exit, i);
    }
}
