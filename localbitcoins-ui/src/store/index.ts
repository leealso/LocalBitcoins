import { configureStore } from '@reduxjs/toolkit'
import { authApi } from '../services/authApiService'
import { localBitcoinsApi } from '../services/localBitcoinsApiService'
import rootReducer from './reducers/index'

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware()
        .concat(authApi.middleware)
        .concat(localBitcoinsApi.middleware)
})

export default store;
export type RootState = ReturnType<typeof store.getState>