require '../lib/etsm.rb'

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
        @output = ''
    end

    def TickA
        @output += ' A '
    end

    def TickB
        @output += ' B '
    end

    def Tick
        if @sm.current != nil and @sm.current.tick != nil
            @sm.current.tick.call
        end
    end

    def Test()
        raise "state != nil" unless @sm.IsIn(nil)
        self.Tick

        @sm.Transition(@a)
        raise "state != A" unless @sm.IsIn(@a)
        self.Tick

        @sm.Transition(@b)
        raise "state != B" unless @sm.IsIn(@b)
        self.Tick

        @sm.Transition(nil)
        raise "state != nil" unless @sm.IsIn(nil)
        self.Tick

        raise 'Output != " A  B "' unless @output == ' A  B '
    end
end

foo = Foo.new
foo.Test()

