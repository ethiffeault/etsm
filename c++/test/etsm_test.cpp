#include <etsm.h>

#include <iostream>
#include <assert.h>
#include <string>

using namespace etsm;

namespace test_ab
{
    class Foo
    {
    public:
        Foo()
            : sm(this),
              a(&Foo::EnterA, &Foo::ExitA),
              b(&Foo::EnterB, nullptr)
        {
        }

        void Test()
        {
            assert(sm.IsIn(nullptr));
            sm.Transition(&a);
            assert(sm.IsIn(&a));
            sm.Transition(&b);
            assert(sm.IsIn(&b));
            sm.Transition(nullptr);
            assert(sm.IsIn(nullptr));
            assert(output == " ->A  A->  ->B ");
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
}

namespace test_virtual_call
{
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

        void Test()
        {
            Tick();
            sm.Transition(&a);
            Tick();
            sm.Transition(&b);
            Tick();
            sm.Transition(nullptr);
            assert(output == " A  B ");
        }

    private:
        StateMachine<Foo, FooState> sm;
        FooState a;
        FooState b;
        std::string output;

        void TickA() { output += " A "; }
        void TickB() { output += " B "; }
    };
}

int main()
{
    test_ab::Foo().Test();
    test_virtual_call::Foo().Test();
}