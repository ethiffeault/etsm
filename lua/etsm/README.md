[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# lua etsm
Tiny state machine for lua, see [etsm](https://github.com/ethiffeault/etsm)

# Description
Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features: 

- states on object (owner)
- optional enter/exit methods
- virtual state user methods
- is in
- unrestricted transitions
- no runtime allocation

# Install

Or simply drop this file into your codebase: [etsm.lua]()

# Example

## Simple

Output: " ->A  A-> ->B "\
Sample: [ab]()

## Virtual State Methods

Output: " A   B "\
Sample: [virtual_call]()
