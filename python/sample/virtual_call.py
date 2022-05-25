import etsm

class FooState(etsm.State):
    def __init__(self, enter, exit, tick):
        super().__init__(enter, exit)
        self.Tick = tick

class Foo:
    def __init__(self):
        self.__sm = etsm.StateMachine(self)
        self.__a = FooState(None, None, Foo.TickA)
        self.__b = FooState(None, None, Foo.TickB)

    def TickA(self):
        print(' A ', end='')

    def TickB(self):
        print(' B ', end='')

    # call tick on the current state, aka virtual call
    def Tick(self):
        if ( self.__sm.Current != None ):
            self.__sm.Current.Tick(self)

    def Run(self):
        self.Tick();
        self.__sm.Transition(self.__a);
        self.Tick();
        self.__sm.Transition(self.__b);
        self.Tick();
        self.__sm.Transition(None);
        self.Tick();

foo = Foo()
foo.Run()