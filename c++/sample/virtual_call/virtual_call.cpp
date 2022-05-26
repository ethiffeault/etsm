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

// Output: " A  B "
int main()
{
	Foo foo;
	foo.Run();
}
