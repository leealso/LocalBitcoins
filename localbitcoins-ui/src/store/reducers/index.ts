import { combineReducers } from "redux";
import tradeReducer from './tradeSlice';
import { authApi } from '../../services/authApiService'
import { localBitcoinsApi } from '../../services/localBitcoinsApiService'

export default combineReducers({
    trades: tradeReducer,
    [authApi.reducerPath]: authApi.reducer,
    [localBitcoinsApi.reducerPath]: localBitcoinsApi.reducer
})