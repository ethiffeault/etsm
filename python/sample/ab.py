import etsm

class Foo:
    def __init__(self):
        self.__sm = etsm.StateMachine(self)
        self.__a = etsm.State(Foo.EnterA, Foo.ExitA)
        self.__b = etsm.State(Foo.EnterB, None)

    def EnterA(self):
        print(' ->A ', end='')

    def ExitA(self):
        print(' A-> ', end='')

    def EnterB(self):
        print(' ->B ', end='')

    # no exit for B
    # def ExitB(self):
    #     print(' B-> ', end='')

    def Run(self):
        self.__sm.Transition(self.__a);
        self.__sm.Transition(self.__b);
        self.__sm.Transition(None);

foo = Foo()
foo.Run()