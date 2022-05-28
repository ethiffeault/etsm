-- MIT License
--
-- Copyright (c) 2022 Eric Thiffeault
--
-- Permission is hereby granted, free of charge, to any person obtaining a copy
-- of this software and associated documentation files (the "Software"), to deal
-- in the Software without restriction, including without limitation the rights
-- to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
-- copies of the Software, and to permit persons to whom the Software is
-- furnished to do so, subject to the following conditions:
--
-- The above copyright notice and this permission notice shall be included in all
-- copies or substantial portions of the Software.
--
-- THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
-- IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
-- FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
-- AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
-- LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
-- OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
-- SOFTWARE.
--
-- https://github.com/ethiffeault/etsm

State = {}

function State:new(enter, exit)
    self.__index = self
    return setmetatable({
        enter = enter,
        exit = exit
    }, self)
end

function State:Enter(owner)
    self:callMethod(owner, self.enter)
end

function State:Exit(owner)
    self:callMethod(owner, self.exit)
end

function State:callMethod(owner, method)
    if (method ~= nil)
    then
        method(owner)
    end
end

StateMachine = {}

function StateMachine:new(owner)
    self.__index = self
    return setmetatable({
        owner = owner
    }, self)
end

function StateMachine:Transition(state)
    if (self.inTransition == true)
    then
        return false;
    end

    self.inTransition = true

    if (self.current ~= nil)
    then
        self.current:Exit(self.owner)
    end

    self.current = state;

    if (self.current ~= nil)
    then
        self.current:Enter(self.owner)
    end

    self.inTransition = false

    return true
end

function StateMachine:IsIn(state)
    return self.current == state
end

return {
    State = State,
    StateMachine = StateMachine
}
