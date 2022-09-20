import { configureStore } from '@reduxjs/toolkit'
import { localBitcoinsApi } from '../services/localBitcoinsService'
import rootReducer from './reducers/index'

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(localBitcoinsApi.middleware),
})

/*export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch*/

export default store;