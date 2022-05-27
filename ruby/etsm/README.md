[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# ruby etsm
Tiny state machine for ruby, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

install the gem:
```
gem install etsm
```
require etsm in your code:
```
require 'etsm'
```

# Example

## Simple

```ruby
require 'etsm'

class Foo
    def initialize
        @sm = Etsm::StateMachine.new(self)
        @a = Etsm::State.new(self.method(:EnterA), self.method(:ExitA))
        @b = Etsm::State.new(self.method(:EnterB), nil)
    end

    def EnterA()
        print ' ->A '
    end

    def ExitA()
        print ' A-> '
    end

    def EnterB()
        print ' ->B '
    end

    # no exit for B
    # def ExitB()
    #     @output += ' B-> '
    # end

    def Test()
        @sm.Transition(@a)
        @sm.Transition(@b)
        @sm.Transition(nil)
    end
end

foo = Foo.new
foo.Test()
```

Output: " ->A  A-> ->B "\
Sample: [ab](https://github.com/ethiffeault/etsm/blob/main/ruby/sample/ab.rb)

## Virtual State Methods

```ruby
require 'etsm.rb'

class FooState < Etsm::State
    attr_reader :tick

    def initialize(enter, exit, tick)
        super(enter, exit)
        @tick = tick
    end
end

class Foo
    def initialize
        @sm = Etsm::StateMachine.new(self)
        @a = FooState.new(nil, nil, self.method(:TickA))
        @b = FooState.new(nil, nil, self.method(:TickB))
    end

    def TickA
        print ' A '
    end

    def TickB
        print ' B '
    end

    def Tick
        if @sm.current != nil and @sm.current.tick != nil
            @sm.current.tick.call
        end
    end

    def Test()
        self.Tick
        @sm.Transition(@a)
        self.Tick
        @sm.Transition(@b)
        self.Tick
        @sm.Transition(nil)
        self.Tick
    end
end

foo = Foo.new
foo.Test()
```

Output: " A   B "\
Sample: [virtual_call]()
