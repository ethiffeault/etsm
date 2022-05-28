[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# lua etsm
Tiny state machine for lua, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

drop this file into your codebase: [etsm.lua](https://github.com/ethiffeault/etsm/blob/main/lua/etsm/etsm.lua)

# Example

## Simple

```lua
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
```

Output: " ->A  A-> ->B "\
Sample: [ab]()

## Virtual State Methods

```lua
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
```

Output: " A   B "\
Sample: [virtual_call]()
