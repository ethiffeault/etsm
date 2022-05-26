[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# c++ etsm
Tiny state machine for c++, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

```cpp
#include <etsm.h>
```  
# Example

## Simple

```c++
#include <etsm.h>
#include <iostream>

using namespace etsm;

class Foo
{
public:
    Foo()
        : sm(this),
        a(&Foo::EnterA, &Foo::ExitA),
        b(&Foo::EnterB, nullptr)
    {
    }

    void Run()
    {
        sm.Transition(&a);
        sm.Transition(&b);
        sm.Transition(nullptr);
        std::cout << output << std::endl;
    }

private:
    StateMachine<Foo> sm;
    State<Foo> a;
    State<Foo> b;
    std::string output;

    void EnterA() { output += " ->A "; }
    void ExitA() { output += " A-> "; }
    void EnterB() { output += " ->B "; }
};

int main()
{
    Foo foo;
    foo.Run();
}
```

Output: " ->A  A->  ->B "\
Sample: [ab](https://github.com/ethiffeault/etsm/tree/main/c%2B%2B/sample/ab)

## Virtual State Methods

```c++
#include <etsm.h>
#include <iostream>

using namespace etsm;

class Foo;

class FooState : public State<Foo>
{
public:
    FooState(Method enter, Method exit, Method tick = nullptr)
        : State<Foo>(enter, exit),
        tick(tick)
    {}

    void Tick(Foo* owner)
    {
        if (tick != nullptr)
            (owner->*tick)();
    }

private:
    Method tick;
};

class Foo
{
public:
    Foo()
        : sm(this),
        a(nullptr, nullptr, &Foo::TickA),
        b(nullptr, nullptr, &Foo::TickB)
    {}

    void Tick()
    {
        if (sm.GetCurrent())
            sm.GetCurrent()->Tick(this);
    }

    void Run()
    {
        Tick();
        sm.Transition(&a);
        Tick();
        sm.Transition(&b);
        Tick();
        sm.Transition(nullptr);
        std::cout << output << std::endl;
    }

private:
    StateMachine<Foo, FooState> sm;
    FooState a;
    FooState b;
    std::string output;

    void TickA() { output += " A "; }
    void TickB() { output += " B "; }
};

int main()
{
    Foo foo;
    foo.Run();
}
```

Output: " A  B "\
Sample: [virtual_call](https://github.com/ethiffeault/etsm/tree/main/c%2B%2B/sample/virtual_call)
