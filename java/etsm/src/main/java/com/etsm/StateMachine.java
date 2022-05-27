//  MIT License
//  
//  Copyright (c) 2022 Eric Thiffeault
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
//
// https://github.com/ethiffeault/etsm

package com.etsm;

public class StateMachine {
    public boolean Transition(IState state) {
        if (inTransition)
            return false;

        inTransition = true;

        if (current != null)
            current.Exit();

        current = state;

        if (current != null)
            current.Enter();

        inTransition = false;

        return true;
    }

    public boolean IsIn(IState state) {
        return current == state;
    }

    public IState GetCurrent() {
        return current;
    }

    private IState current = null;
    private boolean inTransition = false;
}