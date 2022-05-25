import unittest
import etsm

class FooState(etsm.State):
    def __init__(self, enter, exit, tick):
        super().__init__(enter, exit)
        self.Tick = tick

class Foo:
    def __init__(self):
        self.__sm = etsm.StateMachine(self)
        self.__a = FooState(Foo.EnterA, Foo.ExitA, Foo.TickA)
        self.__b = FooState(Foo.EnterB, None, Foo.TickB)
        self.__output = ''

    def EnterA(self):
        self.__output += ' ->A '

    def ExitA(self):
        self.__output += ' A-> '

    def TickA(self):
        self.__output += ' A '

    def EnterB(self):
        self.__output += ' ->B '

    # no exit for B
    # def ExitB(self):
    #     self.__output += ' B-> '

    def TickB(self):
        self.__output += ' B '

    # call tick on the current state, aka virtual call
    def Tick(self):
        if ( self.__sm.Current != None ):
            self.__sm.Current.Tick(self)

    def Test(self, test):
        self.Tick();
        self.__sm.Transition(self.__a);
        test.assertEqual(self.__sm.IsIn(self.__a), True)
        self.Tick();
        self.__sm.Transition(self.__b);
        test.assertEqual(self.__sm.IsIn(self.__b), True)
        self.Tick();
        self.__sm.Transition(None);
        self.Tick();
        test.assertEqual(self.__output, ' ->A  A  A->  ->B  B ')

class TestMethods(unittest.TestCase):

    def test_sample(self):
        foo = Foo()
        foo.Test(self)

if __name__ == '__main__':
    unittest.main()