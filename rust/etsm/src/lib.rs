//! MIT License
//!
//! Copyright (c) 2022 Eric Thiffeault
//!
//! Permission is hereby granted, free of charge, to any person obtaining a copy
//! of this software and associated documentation files (the "Software"), to deal
//! in the Software without restriction, including without limitation the rights
//! to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//! copies of the Software, and to permit persons to whom the Software is
//! furnished to do so, subject to the following conditions:
//!
//! The above copyright notice and this permission notice shall be included in all
//! copies or substantial portions of the Software.
//!
//! THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//! IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//! FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//! AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//! LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//! OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//! SOFTWARE.
//! 
//! <https://github.com/ethiffeault/etsm>

/*
// example A B
  
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

// Output: " ->A A-> ->B "
fn simple() {
    let mut foo = Foo::new();
    foo.run();
}
*/

pub enum EmptyStateData {}

pub struct StateCallback<OWNER, STATEDATA = EmptyStateData> {
    pub enter: Option<fn(&mut OWNER)>,
    pub exit: Option<fn(&mut OWNER)>,
    pub statedata: Option<STATEDATA>,
}

impl<OWNER, STATEDATA> StateCallback<OWNER, STATEDATA> {
    pub fn new(
        enter: Option<fn(&mut OWNER)>,
        exit: Option<fn(&mut OWNER)>,
        statedata: Option<STATEDATA>,
    ) -> Self {
        StateCallback {
            enter: enter,
            exit: exit,
            statedata: statedata,
        }
    }
}

pub trait StateSelector<OWNER, STATEDATA = EmptyStateData> {
    fn select(&self) -> StateCallback<OWNER, STATEDATA>;
}

pub struct StateMachine<STATE> {
    pub current_state: Option<STATE>,
    pub in_transition : bool
}

impl<STATE: PartialEq> StateMachine<STATE> {
    pub fn new() -> StateMachine<STATE> {
        StateMachine {
            current_state: None,
            in_transition: false
        }
    }

    pub fn is_in(&self, state: Option<STATE>) -> bool {
        if let Some(c) = &self.current_state {
            if let Some(s) = state {
                return *c == s;
            } else {
                return false;
            }
        } else {
            if let Some(_s) = state {
                return false;
            } else {
                return true;
            }
        }
    }
}

/// Get current state data
/// 
/// # Example
/// 
/// if let Some(statedata) = get_statedata(&self.state_machine) {
///     (statedata.run)(self);
/// }
pub fn get_statedata<STATEDATA, OWNER, STATE: StateSelector<OWNER, STATEDATA>>(
    state_machine: &StateMachine<STATE>,
) -> Option<STATEDATA> {
    if let Some(c) = &state_machine.current_state {
        return c.select().statedata;
    }
    return None;
}

/// Declare state machine
///
/// # Example
/// 
/// state_machine!(
///     Foo,
///     State {
///         {A, Some(Foo::enter_a), Some(Foo::exit_a)},
///         {B, Some(Foo::enter_b), None}
///     }
/// );
#[macro_export]
macro_rules! state_machine {
    ($owner_type:ident , $state_type:ident {$({$state_name: ident , $enter: expr , $exit: expr}),*} ) => {
    #[derive(PartialEq)]
    #[derive(Clone)]
    #[derive(Debug)]
    enum $state_type {
        $($state_name),*
    }
    impl StateSelector<$owner_type> for $state_type {
            fn select(&self) -> StateCallback<$owner_type> {
                match self {
                    $($state_type::$state_name => StateCallback::new($enter, $exit, None)),*
                }
            }
        }
    };
    ($owner_type:ident , $state_data:ident , $state_type:ident {$({$state_name: ident , $enter: expr , $exit: expr, $data: expr}),*} ) => {
        #[derive(PartialEq)]
        #[derive(Clone)]
        #[derive(Debug)]
        enum $state_type {
            $($state_name),*
        }
        impl StateSelector<$owner_type, $state_data> for $state_type {
                fn select(&self) -> StateCallback<$owner_type, $state_data> {
                    match self {
                        $($state_type::$state_name => StateCallback::new($enter, $exit, $data)),*
                    }
                }
            }
        };
}

/// Make a transition
#[macro_export]
macro_rules! transition {
    ($owner:expr, $state_machine:ident , $state:expr) => {{

        let mut result = false;
        if ($owner.$state_machine.in_transition == false ) {

            $owner.$state_machine.in_transition = true;
            
            if let Some(s) = &$owner.state_machine.current_state {
                if let Some(c) = s.select().exit {
                    (c)($owner);
                }
            }

            $owner.$state_machine.current_state = $state;

            if let Some(s) = &$owner.state_machine.current_state {
                if let Some(c) = s.select().enter {
                    (c)($owner);
                }
            }

            $owner.$state_machine.in_transition = false;
            result = true;
        }   
        result
    }};
}
