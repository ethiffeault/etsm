[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/etsm.svg)](https://www.nuget.org/packages/etsm)
[![Version](https://img.shields.io/crates/v/etsm.svg)](https://crates.io/crates/etsm)

# etsm
Efficient Tiny State Machine using object callbacks. Implemented in many languages. 

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Why
For small project or need of small and simple state machine.

# Install
Depend on the language, etsm is designed to be a one file dropper in your project. A package might also be available.

# Languages
- [rust](rust/etsm) 
- [c#](cs)
- [go](go)
