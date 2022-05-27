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
foo.Test() # Output: ' ->A  A->  ->B '


