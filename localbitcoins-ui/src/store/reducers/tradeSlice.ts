import { createSlice } from '@reduxjs/toolkit';
import { Trade } from '../../types/trade'

interface TradeState {
  selectedDate: number,
  trades: Array<Trade>,
  loading: string,
  currentRequestId: any
}

const initialState = {
  selectedDate: new Date().getTime(),
  trades: [],
  //loading: REQUEST_STATE.IDLE,
  currentRequestId: undefined
} as TradeState

/*

const fetchTrades = createAsyncThunk<Trade, string, {
  state: { trade: { loading: string, currentRequestId: string } }
}>(
  'trade/fetchTrades',
  async (_, { getState, requestId }) => {
    const { currentRequestId, loading } = getState().trade
    if (loading !== REQUEST_STATE.PENDING || requestId !== currentRequestId) {
      return
    }
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
    return response.data.data.trades.nodes
  }
)*/

/*export const fetchTrades = () => async dispatch => {
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
}*/

export const tradeSlice = createSlice({
  name: 'trade',
  initialState,
  reducers: {
    setSelectedDate: (state, action) => {
      state.selectedDate = action.payload
    },
    setTrades: (state, action) => {
      state.trades = action.payload
    }
  }
})

export const { setSelectedDate, setTrades } = tradeSlice.actions

export default tradeSlice.reducer