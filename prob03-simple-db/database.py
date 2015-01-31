#!/usr/bin/python3
# I like to call this one "Stack is cheap, bitches."
import sys
from copy import copy

NULL = 'NULL'
NO_TRANSACTION = 'NO_TRANSACTION'
open_trans = -1

def _begin(state, counts, *args):
    global open_trans
    open_trans += 1
    new_state = copy(state)
    new_counts = copy(counts)
    while(True):
        a, var, val = get_next_line()
        if(a == 'ROLLBACK'):
            if(open_trans > 0):
                open_trans -= 1
                return state, counts, False
            else:
                print(NO_TRANSACTION)
        elif(a == 'COMMIT'):
            if(open_trans > 0):
                open_trans -= 1
                return new_state, new_counts, True
            else:
                print(NO_TRANSACTION)
        elif(a == 'END'):
            exit()
        else:
            new_state, new_counts, commit = actions[a](new_state, new_counts, var, val)
            # close out all transactions if commit
            if(commit and open_trans > 0):
                open_trans -= 1
                return new_state, new_counts, True

    return new_state, new_counts

# Utility
def get_next_line():
    seq = sys.stdin.readline().strip().split(' ')
    while(len(seq) < 3):
        seq.append(None)
    return seq

def _get(state, counts, var, _):
    try:
        print(state[var])
    except KeyError:
        print(NULL)
    return state, counts, False

def _set(state, counts, var, val):
    try:
        cur_val = state[var]
        counts[cur_val] -= 1
    except KeyError:
        pass
    state[var] = val
    try:
        counts[val] += 1
    except KeyError:
        counts[val] = 1
    return state, counts, False

def _uset(state, counts, var, _):
    try:
        val = state[var]
        del state[var]
        counts[val] -= 1
    except KeyError:
        pass
    return state, counts, False

def _numeq(state, counts, val, _):
    count = 0
    try:
        count = counts[val]
    except KeyError:
        pass
    print(count)
    return state, counts, False

# Map inputs to python functions
actions = {
    'GET': _get,
    'SET': _set,
    'UNSET': _uset,
    'NUMEQUALTO': _numeq,
    'BEGIN': _begin,
}

_begin({}, {}, None, None)
