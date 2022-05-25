package main

import (
	"fmt"
	"strings"
)

type State interface {
}

type Enter interface {
	Enter()
}

type Exit interface {
	Exit()
}

type StateMachine struct {
	current State
}

func (sm *StateMachine) Transition(state State) {
	if sm.current != nil {
		i, ok := sm.current.(Exit)
		if ok {
			i.Exit()
		}
	}
	sm.current = state
	if sm.current != nil {
		i, ok := sm.current.(Enter)
		if ok {
			i.Enter()
		}
	}
}

func (sm *StateMachine) IsIn(state State) bool {
	return sm.current == state
}

type StateA struct {
	foo *Foo
}

func (s *StateA) Enter() {
	s.foo.Trace(" <-A ")
}
func (s *StateA) Exit() {
	s.foo.Trace(" A-> ")
}

func (s *StateA) Tick() {
	s.foo.Trace(" A ")
}

type StateB struct {
	foo *Foo
}

func (s *StateB) Enter() {
	s.foo.Trace(" ->B ")
}

func (s *StateB) Exit() {
	s.foo.Trace(" B-> ")
}

func (s *StateB) Tick() {
	s.foo.Trace(" B ")
}

type Tick interface {
	Tick()
}

type Foo struct {
	stateA       StateA
	stateB       StateB
	stateMachine StateMachine
	output       strings.Builder
}

func NewFoo() *Foo {
	f := new(Foo)
	f.stateMachine = StateMachine{}
	f.stateA = StateA{f}
	f.stateB = StateB{f}
	return f
}

func (f *Foo) Trace(s string) {
	f.output.WriteString(s)
}

func (f *Foo) Tick() {
	if f.stateMachine.current != nil {
		tick, ok := f.stateMachine.current.(Tick)
		if ok {
			tick.Tick()
		}
	}
}

func (f *Foo) Test() {
	f.stateMachine.Transition(&f.stateA)
	f.Tick()
	f.stateMachine.Transition(&f.stateB)
	f.Tick()
	f.stateMachine.Transition(nil)
}

func main() {
	var foo = NewFoo()
	foo.Test()
	fmt.Println(foo.output.String())
}
