import { combineReducers } from 'redux'
import authReducer from './authSlice'
import tradeReducer from './tradeSlice'
import { authApi } from '../../services/authApiService'
import { localBitcoinsApi } from '../../services/localBitcoinsApiService'

export default combineReducers({
    trades: tradeReducer,
    auth: authReducer,
    [authApi.reducerPath]: authApi.reducer,
    [localBitcoinsApi.reducerPath]: localBitcoinsApi.reducer
})