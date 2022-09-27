import { configureStore } from '@reduxjs/toolkit'
import { localBitcoinsApi } from '../services/localBitcoinsApiService'
import rootReducer from './reducers/index'

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(localBitcoinsApi.middleware)
})

export default store;