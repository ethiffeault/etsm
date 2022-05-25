// MIT License
//
// Copyright (c) 2022 Eric Thiffeault
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// https://github.com/ethiffeault/etsm
package etsm

// State interface
type State interface {
}

// Optional interface for Enter transition callback
type Enter interface {
	Enter()
}

// Optional interface for Exit transition callback
type Exit interface {
	Exit()
}

// StateMachine, to embed into a user struct
type StateMachine struct {
	Current State
}

// call to make transition from state to state, state might be nil
func (sm *StateMachine) Transition(state State) {
	if sm.Current != nil {
		i, ok := sm.Current.(Exit)
		if ok {
			i.Exit()
		}
	}
	sm.Current = state
	if sm.Current != nil {
		i, ok := sm.Current.(Enter)
		if ok {
			i.Enter()
		}
	}
}

// check if a state machine is in a state,
func (sm *StateMachine) IsIn(state State) bool {
	return sm.Current == state
}
