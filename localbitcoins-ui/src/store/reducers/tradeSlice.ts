import { createSlice } from '@reduxjs/toolkit'

interface TradeState {
  selectedDate: number,
}

const initialState = {
  selectedDate: new Date().getTime(),
} as TradeState

export const tradeSlice = createSlice({
  name: 'trade',
  initialState,
  reducers: {
    setSelectedDate: (state, action) => {
      state.selectedDate = action.payload
    }
  }
})

export const { setSelectedDate } = tradeSlice.actions

export default tradeSlice.reducer