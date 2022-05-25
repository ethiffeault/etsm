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
