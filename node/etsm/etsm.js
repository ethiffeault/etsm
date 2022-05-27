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

class State {
    constructor(enter, exit) {
        this.Enter = enter;
        this.Exit = exit;
    }
}

class StateMachine {
    constructor(owner) {
        this.owner = owner;
        this.current = null;
        this.in_transition = false;
    }

    Transition(state) {
        if ( this.in_transition )
            return false;

        this.in_transition = true;

        if ( this.current != null && this.current.Exit != null) 
            this.current.Exit.apply(this.owner);

        this.current = state;

        if ( this.current != null && this.current.Enter != null) 
            this.current.Enter.apply(this.owner);

        this.in_transition = false;

        return true;
    }

    IsIn(state) {
        return this.current == state;
    }
}

module.exports.State = State;
module.exports.StateMachine = StateMachine;
