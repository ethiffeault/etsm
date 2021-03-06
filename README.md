[![](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![](https://img.shields.io/nuget/v/etsm.svg)](https://www.nuget.org/packages/etsm)
[![](https://img.shields.io/crates/v/etsm.svg)](https://crates.io/crates/etsm)
[![](https://badge.fury.io/py/etsm.svg)](https://badge.fury.io/py/etsm)
[![](https://badge.fury.io/rb/etsm.svg)](https://badge.fury.io/rb/etsm)
[![npm version](https://badge.fury.io/js/etsm.svg)](https://badge.fury.io/js/etsm)

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

# Languages
- [c++](c++)
- [c#](cs)
- [go](go)
- [java](java/etsm)
- [lua](lua/etsm)
- [node](node/etsm)
- [python](python/etsm)
- [ruby](ruby/etsm)
- [rust](rust/etsm) 

# Why
For small and simple state machine needs. Source code for each language is very small, easy to copy/paste...\
Also because it's funny to see how a little bit of code can be different from language to language.

# Install
Depend on the language, etsm is designed to be a one file dropper in your project. But a package might also be available.
