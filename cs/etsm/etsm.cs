﻿//  MIT License
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

namespace Etsm
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public class State : IState
    {
        private Action? enter;
        private Action? exit;

        public State(Action? enter, Action? exit)
        {
            this.enter = enter;
            this.exit = exit;
        }

        public void Enter()
        {
            enter?.Invoke();
        }

        public void Exit()
        {
            exit?.Invoke();
        }
    }

    public class StateMachine<STATE> where STATE : class, IState
    {
        public STATE? CurrentState { get; private set; }

        public void Transition(STATE? state)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = state;

            if (CurrentState != null)
                CurrentState.Enter();
        }

        public bool IsIn(STATE? s)
        {
            return CurrentState == s;
        }
    }
}