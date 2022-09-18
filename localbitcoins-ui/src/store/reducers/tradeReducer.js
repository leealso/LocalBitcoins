import { FETCH_TRADES } from '../actions/types';

const initialState = {
    trades: []
};

export default function (state = initialState, action) {
    switch (action.type) {
        case FETCH_TRADES:
            console.log(`${FETCH_TRADES} action`);
            return {
                ...state,
                trades: action.payload
            };
        default:
            return state;
    }
}