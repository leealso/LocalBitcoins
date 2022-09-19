import { createSlice } from '@reduxjs/toolkit';
import { getTradesQuery } from './graphQlQueries';
import axios from 'axios';

export const fetchTrades = () => async dispatch => {
  try {
      const response = await axios({
          url: 'https://localbitcoinsapi.azurewebsites.net/graphql',
          method: 'post',
          data: {
              query: getTradesQuery,
              variables: {
                where: {
                    date: {
                        gte: '2022-09-19T06:00:00.000Z'
                    }
                }
            }
          }
      })
      dispatch(setTrades(response.data));
  }
  catch (error) {
      console.log('error')
  }
}

export const tradeSlice = createSlice({
  name: 'trade',
  initialState: {
    selectedDate: new Date().getTime(),
    trades: []
  },
  reducers: {
    setSelectedDate: (state, action) => {
      console.log(action.payload)
      state.selectedDate = action.payload
    },
    setTrades: (state, action) => {
      state.trades = action.payload.data.trades.nodes
    }
  },
})

export const { setSelectedDate, setTrades } = tradeSlice.actions

export default tradeSlice.reducer