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

namespace etsm {

    template<typename OWNER>
    class State
    {
    public:
        typedef void (OWNER::* Method)();

        State(Method enter, Method exit)
            : enter(enter),
              exit(exit)
        {}

        void Enter(OWNER* owner) const
        {
            if (enter != nullptr)
                (owner->*enter)();
        }

        void Exit(OWNER* owner) const
        {
            if (exit != nullptr)
                (owner->*exit)();
        }

    private:
        Method enter;
        Method exit;
    };

    template<typename OWNER, typename STATE = State<OWNER>>
    class StateMachine
    {
    public:
        StateMachine(OWNER* owner)
            : owner(owner)
        {}

        STATE* GetCurrent()
        {
            return current;
        }

        bool Transition(STATE* state)
        {
            // cannot do transition inside Enter/Exit
            if (inTransition)
                return false;

            inTransition = true;
            if (current != nullptr)
                current->Exit(owner);

            current = state;

            if (current != nullptr)
                current->Enter(owner);

            inTransition = false;

            return true;
        }

        bool IsIn(const STATE* state)
        {
            return current == state;
        }

    private:
        STATE* current = nullptr;
        OWNER* owner = nullptr;
        bool inTransition = false;
    };
}