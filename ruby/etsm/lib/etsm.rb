# MIT License
#
# Copyright (c) 2022 Eric Thiffeault
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.
# 
# <https://github.com/ethiffeault/etsm>

module Etsm

    class State
        attr_reader :enter
        attr_reader :exit

        def initialize(enter, exit)
            @enter = enter
            @exit = exit
        end
    end

    class StateMachine

        attr_reader :owner
        attr_reader :current
        attr_reader :in_transition

        def initialize(owner)
            @owner = owner
            @current = nil
            @in_transition = false
        end

        def Transition(state)
            if @in_transition == true
                return false
            end

            @in_transition = true

            if @current != nil and @current.exit != nil
                @current.exit.call
            end

            @current = state

            if @current != nil and @current.enter != nil
                @current.enter.call
            end

            @in_transition = false
            return true
        end

        def IsIn(state)
            return state == @current
        end

    end
end # module etsm