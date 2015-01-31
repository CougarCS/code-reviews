#!/usr/bin/python3
# I like to call this one "Stack is cheap, bitches."
import sys
from copy import copy

NULL = 'NULL'
NO_TRANSACTION = 'NO_TRANSACTION'
open_trans = -1

def _begin(state, *args):
    global open_trans
    open_trans += 1
    new_state = copy(state)
    while(True):
        a, var, val = get_next_line()
        if(a == 'ROLLBACK'):
            if(open_trans > 0):
                open_trans -= 1
                return state
            else:
                print(NO_TRANSACTION)
        elif(a == 'COMMIT'):
            if(open_trans > 0):
                open_trans -= 1
                return new_state
            else:
                print(NO_TRANSACTION)
        elif(a == 'END'):
            exit()
        else:
            new_state = actions[a](new_state, var, val)

    return new_state

# Utility
def get_next_line():
    seq = sys.stdin.readline().strip().split(' ')
    while(len(seq) < 3):
        seq.append(None)
    return seq

def _get(state, var, _):
    try:
        print(state[var])
    except KeyError:
        print(NULL)
    return state

def _set(state, var, val):
    state[var] = val
    try:
        counts[val] += 1
    except KeyError:
        counts[val] = 1
    return state

def _uset(state, var, _):
    try:
        val = state[var]
        del state[var]
        counts[var] -= 1
    except KeyError:
        pass
    return state

def _numeq(state, val, _):
    count = 0
    try:
        count = counts[val]
    except KeyError:
        pass
    print(count)
    return state

# Map inputs to python functions
actions = {
    'GET': _get,
    'SET': _set,
    'UNSET': _uset,
    'NUMEQUALTO': _numeq,
    'BEGIN': _begin,
}

global_state = {}
counts = {}

_begin(global_state, None, None)
exit()
