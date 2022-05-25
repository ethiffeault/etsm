[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# go etsm
Tiny state machine for go, see [etsm](https://github.com/ethiffeault/etsm)

# Install

Drop this file into your project [etsm.go](https://github.com/ethiffeault/etsm/blob/main/go/etsm/etsm.go)
or
Add this package to your go.mod

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Example

```go
package main

import (
	"fmt"
	"strings"

	"github.com/ethiffeault/etsm/go/etsm"
)

// State A
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

// State B
type StateB struct {
	foo *Foo
}

func (s *StateB) Enter() {
	s.foo.Trace(" ->B ")
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
	stateA       StateA
	stateB       StateB
	stateMachine etsm.StateMachine
	output       strings.Builder
}

func NewFoo() *Foo {
	f := new(Foo)
	f.stateMachine = etsm.StateMachine{}
	// construct all possible states
	f.stateA = StateA{f}
	f.stateB = StateB{f}
	return f
}

func (f *Foo) Trace(s string) {
	f.output.WriteString(s)
}

func (f *Foo) Tick() {
	if f.stateMachine.Current != nil {
		tick, ok := f.stateMachine.Current.(Tick)
		if ok {
			tick.Tick()
		}
	}
}

func (f *Foo) Test() {
	f.Tick()
	f.stateMachine.Transition(&f.stateA)
	f.Tick()
	f.stateMachine.Transition(&f.stateB)
	f.Tick()
	f.stateMachine.Transition(nil)
	f.Tick()
	fmt.Println(f.output.String())
}

func main() {
	var foo = NewFoo()
	foo.Test()
}
```
output: " <-A  A  A->  ->B  B "

full sample [here](https://github.com/ethiffeault/etsm/blob/main/go/sample/sample.go)
