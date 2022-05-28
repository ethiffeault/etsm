package = "etsm"
version = "0.5-3"
source = {
   url = "https://github.com/ethiffeault/etsm/tree/main/lua/etsm",
   tag = "0.5.3"
}
description = {
   summary = "Efficient Tiny State Machine using object callbacks.",
   detailed = [[
      Implement a bare bones state machine in many languages. This library aim to be simple as possible and support only basic features:
      states on object (owner), optional enter/exit methods, virtual state user methods, is in, unrestricted transitions, no runtime allocation
   ]],
   homepage = "https://github.com/ethiffeault/etsm",
   license = "MIT"
}
dependencies = {
   "lua >= 5.1, < 5.4"
}
build = {
   type = "builtin",
   modules = {
      etsm = "etsm.lua",
   }
}