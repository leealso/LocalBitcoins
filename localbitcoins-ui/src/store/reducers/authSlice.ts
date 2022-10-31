import { createSlice } from '@reduxjs/toolkit'
import { authApi } from '../../services/authApiService'
import type { RootState } from '../index'

export interface AuthState {
  access_token: string
  id_token: string
  user_id: string
  is_authenticated: boolean
}

const initialState = {
  access_token: null,
  id_token: null,
  user_id: null,
  is_authenticated: false,
} as AuthState

const slice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: () => initialState,
  },
  extraReducers: (builder) => {
    builder
      .addMatcher(authApi.endpoints.getToken.matchFulfilled, (state, action) => {
        state.access_token = action.payload.access_token
        state.id_token = action.payload.id_token
        state.user_id = action.payload.user_id
        state.is_authenticated = true
      })
  },
})

export const { logout } = slice.actions
export default slice.reducer

export const selectIsAuthenticated = (state: RootState) =>
  state.auth.is_authenticated

export const selectAccessToken = (state: RootState) =>
  state.auth.access_token
