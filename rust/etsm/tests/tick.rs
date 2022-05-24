use etsm::*;

state_machine!(
    Foo,
    Data,
    State {
        {A, None, None, Some(Data::new(Foo::tick_a))},
        {B, None, None, Some(Data::new(Foo::tick_b))}
    }
);

struct Data {
    tick: fn(&mut Foo),
}

impl Data {
    fn new(tick: fn(&mut Foo)) -> Data {
        Data { tick: tick }
    }
}

struct Foo {
    state_machine: StateMachine<State>,
    state_a_tick: i32,
    state_b_tick: i32,
}

impl Foo {
    fn new() -> Foo {
        Foo {
            state_machine: StateMachine::new(),
            state_a_tick: 0,
            state_b_tick: 0,
        }
    }

    fn tick_a(&mut self) {
        self.state_a_tick += 1;
    }

    fn tick_b(&mut self) {
        self.state_b_tick += 1;
    }

    fn tick(&mut self) {
        // tick the current state
        if let Some(statedata) = get_statedata(&self.state_machine) {
            (statedata.tick)(self);
        }
    }
}

#[test]
fn tick() {
    let mut foo = Foo::new();

    assert!(foo.state_machine.is_in(None));
    assert_eq!(foo.state_a_tick, 0);
    assert_eq!(foo.state_b_tick, 0);
    assert_eq!(foo.state_machine.current_state, None);

    transition!(&mut foo, state_machine, Some(State::A));
    assert!(foo.state_machine.is_in(Some(State::A)));
    foo.tick();
    assert_eq!(foo.state_a_tick, 1);
    assert_eq!(foo.state_b_tick, 0);

    transition!(&mut foo, state_machine, Some(State::B));
    assert!(foo.state_machine.is_in(Some(State::B)));
    foo.tick();
    assert_eq!(foo.state_a_tick, 1);
    assert_eq!(foo.state_b_tick, 1);

    transition!(&mut foo, state_machine, None);
    assert!(foo.state_machine.is_in(None));
    foo.tick();
    assert_eq!(foo.state_a_tick, 1);
    assert_eq!(foo.state_b_tick, 1);
}
