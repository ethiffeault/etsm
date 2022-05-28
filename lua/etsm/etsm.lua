
State = {}

function State:new(enter, exit)
    self.__index = self
    return setmetatable({
        enter = enter,
        exit = exit
    }, self)
end

function State:Enter(owner)
    self:callMethod(owner, self.enter)
end

function State:Exit(owner)
    self:callMethod(owner, self.exit)
end

function State:callMethod(owner, method)
    if (method ~= nil)
    then
        method(owner)
    end
end

StateMachine = {}

function StateMachine:new(owner)
    self.__index = self
    return setmetatable({
        owner = owner
    }, self)
end

function StateMachine:Transition(state)
    if (self.inTransition == true)
    then
        return false;
    end

    self.inTransition = true

    if (self.current ~= nil)
    then
        self.current:Exit(self.owner)
    end

    self.current = state;

    if (self.current ~= nil)
    then
        self.current:Enter(self.owner)
    end

    self.inTransition = false

    return true
end

function StateMachine:IsIn(state)
    return self.current == state
end

return {
    State = State,
    StateMachine = StateMachine
}
