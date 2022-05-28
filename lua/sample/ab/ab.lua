package.path = './../../etsm/?.lua;' .. package.path
local etsm = require 'etsm'

Foo = {}

function Foo:new(o)
    self.__index = self
    return setmetatable({
        sm = etsm.StateMachine:new(self),
        a = etsm.State:new(Foo.EnterA, Foo.ExitA),
        b = etsm.State:new(Foo.EnterB, nil)
    }, self)
end

function Foo:EnterA()
    io.write(" ->A ")
end

function Foo:ExitA()
    io.write(" A-> ")
end

function Foo:EnterB()
    io.write(" ->B ")
end

-- no exit for state B
-- function Foo:ExitB()
--     io.write(" B-> ")
-- end

function Foo:Test()
    self.sm:Transition(self.a)
    self.sm:Transition(self.b)
    self.sm:Transition(nil)
end

local foo = Foo:new(nil)
foo:Test()
