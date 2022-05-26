package etsm

import (
	"strings"
	"testing"
)

var test *testing.T

// State A
type StateA struct {
	foo *Foo
}

func (s *StateA) Enter() {
	s.foo.Trace(" <-A ")
	if s.foo.sm.Transition(s.foo.a) {
		test.Fatalf(`Cannot do transition inside Enter/Exit`)
	}
}
func (s *StateA) Exit() {
	s.foo.Trace(" A-> ")
	if s.foo.sm.Transition(s.foo.a) {
		test.Fatalf(`Cannot do transition inside Enter/Exit`)
	}
}

func (s *StateA) Tick() {
	s.foo.Trace(" A ")
}

// State B
type StateB struct {
	foo *Foo
}

func (s *StateB) Enter() {
	s.foo.Trace(" ->B ")
	if s.foo.sm.Transition(s.foo.a) {
		test.Fatalf(`Cannot do transition inside Enter/Exit`)
	}
}

// don't want exit callback on state B
// func (s *StateB) Exit() {
// 	s.foo.Trace(" B-> ")
// }

func (s *StateB) Tick() {
	s.foo.Trace(" B ")
}

// Tick interface, aka virual method
type Tick interface {
	Tick()
}

// declate the state machine owner
type Foo struct {
	a      StateA
	b      StateB
	sm     StateMachine
	output strings.Builder
}

func NewFoo() *Foo {
	f := new(Foo)
	f.sm = NewStateMachine()
	// construct all possible states
	f.a = StateA{f}
	f.b = StateB{f}
	return f
}

func (f *Foo) Trace(s string) {
	f.output.WriteString(s)
}

func (f *Foo) Tick() {
	if f.sm.Current != nil {
		tick, ok := f.sm.Current.(Tick)
		if ok {
			tick.Tick()
		}
	}
}

func (f *Foo) Test() {
	f.Tick()
	f.sm.Transition(&f.a)
	f.Tick()
	f.sm.Transition(&f.b)
	f.Tick()
	f.sm.Transition(nil)
	f.Tick()
	o := f.output.String()
	ok := " <-A  A  A->  ->B  B "
	if o != ok {
		test.Fatalf(`Foo.output = %q, %q, want "", error`, o, ok)
	}
}

func TestFoo(t *testing.T) {
	test = t
	var foo = NewFoo()
	foo.Test()
}
