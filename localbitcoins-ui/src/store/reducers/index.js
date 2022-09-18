import { combineReducers } from "redux";
//import closedTradeReducer from './closedTradeReducer';
import tradeReducer from './tradeReducer';

export default combineReducers({
    trades: tradeReducer
    //closedTrades: closedTradeReducer
})