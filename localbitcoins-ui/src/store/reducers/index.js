import { combineReducers } from "redux";
import tradeReducer from './tradeSlice';

export default combineReducers({
    trades: tradeReducer
})