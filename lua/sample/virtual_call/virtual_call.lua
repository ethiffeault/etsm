package.path = './../../etsm/?.lua;' .. package.path
local etsm = require 'etsm'

State = {}
setmetatable(State, {__index = etsm.State})

function State:new(enter, exit, tick)
    self.__index = self
    local o = etsm.State:new(enter, exit)
    o.tick = tick
    return setmetatable(o, self)
end

Foo = {}

function Foo:new(o)
    self.__index = self
    return setmetatable({
        sm = etsm.StateMachine:new(self),
        a = State:new(nil, nil, Foo.TickA),
        b = State:new(nil, nil, Foo.TickB)
    }, self)
end

function Foo:TickA()
    io.write(" A ")
end

function Foo:TickB()
    io.write(" B ")
end

function Foo:Tick()
    if (self.sm.current ~= nil and self.sm.current.tick ~= nil)
    then
        self.sm.current.tick(self)
    end
end

function Foo:Test()
    self:Tick()

    self.sm:Transition(self.a)
    self:Tick()

    self.sm:Transition(self.b)
    self:Tick()

    self.sm:Transition(nil)
    self:Tick()
end

local foo = Foo:new(nil)
foo:Test()
