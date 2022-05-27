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
foo.Test() # Output: " A  B "

