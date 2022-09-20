import { combineReducers } from "redux";
import tradeReducer from './tradeSlice';
import { localBitcoinsApi } from '../../services/localBitcoinsService'

export default combineReducers({
    trades: tradeReducer,
    [localBitcoinsApi.reducerPath]: localBitcoinsApi.reducer
})