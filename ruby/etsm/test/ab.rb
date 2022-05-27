require '../lib/etsm.rb'

class Foo
    def initialize
        @sm = Etsm::StateMachine.new(self)
        @a = Etsm::State.new(self.method(:EnterA), self.method(:ExitA))
        @b = Etsm::State.new(self.method(:EnterB), nil)
        @output = ''
    end

    def EnterA()
        @output += ' ->A '
    end

    def ExitA()
        @output += ' A-> '
    end

    def EnterB()
        @output += ' ->B '
    end

    # no exit for B
    # def ExitB()
    #     @output += ' B-> '
    # end

    def Test()
        raise "state != nil" unless @sm.IsIn(nil)
        @sm.Transition(@a)
        raise "state != A" unless @sm.IsIn(@a)
        @sm.Transition(@b)
        raise "state != B" unless @sm.IsIn(@b)
        @sm.Transition(nil)
        raise "state != nil" unless @sm.IsIn(nil)
        raise 'Output != " ->A  A->  ->B "' unless @output == ' ->A  A->  ->B '
    end
end

foo = Foo.new
foo.Test()

