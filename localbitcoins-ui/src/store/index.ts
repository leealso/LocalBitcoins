import { configureStore } from '@reduxjs/toolkit'
import { localBitcoinsApi } from '../services/localBitcoinsApiService'
import { localBitcoins } from '../services/localBitcoinsService'
import rootReducer from './reducers/index'

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(localBitcoinsApi.middleware)
        .concat(localBitcoins.middleware),
})

export default store;