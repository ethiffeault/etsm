# c# tsm
Tiny state machine for c#, see [etsm](../../../../)

# Example

## Simple A B
Declare state machine
```

```

Instanciate the state machine in your struct
```
```

Add enter/exit callbacks
```
```

Execute transitions
```
```

Output: " ->A  A-> ->B "

full sample [here](tests/simple.rs)

## Virtual State Methods

Declare state machine with data, you might use any type of data, view this data as static data for each state.
```
```

Declare the statedata struct
```
```

Implement Foo
```
```

Call method run on Foo that forward it to the current state
```
```
full sample [here](tests/virtual_call.rs)
