import { FETCH_CLOSED_TRADES } from '../actions/types';

const initialState = {
    closedTrades: []
}

export default function(state = initialState, action) {
    switch(action.type) {
        case FETCH_CLOSED_TRADES:
            state.closedTrades = []
        default: 
            return state
    }
}