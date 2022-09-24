import { createSlice } from '@reduxjs/toolkit'

interface TradeState {
  selectedDate: number,
  pageSize: number,
  selectedPage: number
}

const initialState = {
  selectedDate: new Date().getTime(),
  pageSize: 25,
  selectedPage: 1
} as TradeState

export const tradeSlice = createSlice({
  name: 'trade',
  initialState,
  reducers: {
    setSelectedDate: (state, action) => {
      state.selectedDate = action.payload
      state.selectedPage = 1
    },
    setPageSize: (state, action) => {
      state.pageSize = action.payload
    },
    setSelectedPage: (state, action) => {
      state.selectedPage = action.payload
    }
  }
})

export const { setSelectedDate, setPageSize, setSelectedPage } = tradeSlice.actions

export default tradeSlice.reducer