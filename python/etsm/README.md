[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![PyPI version](https://badge.fury.io/py/etsm.svg)](https://badge.fury.io/py/etsm)

# python etsm
Tiny state machine for python, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install
include this file into your project [etsm.py](https://github.com/ethiffeault/etsm/blob/main/python/etsm/src/etsm.py)\
or\
import it
```python
import etsm
```

# Example

## Simple

```python
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
```
Output : " ->A  A->  ->B "\
Sample: [ab.py](https://github.com/ethiffeault/etsm/blob/main/python/sample/ab.py)

## Virtual State Methods

```python
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
```
Output: " A  B "\
Sample: [virtual_call.py](https://github.com/ethiffeault/etsm/blob/main/python/sample/virtual_call.py)
