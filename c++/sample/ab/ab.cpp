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

// Output: " ->A  A->  ->B "
int main()
{
	Foo foo;
	foo.Run();
}
