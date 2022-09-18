import { FETCH_TRADES } from './types';
import { getTradesQuery } from './graphQlQueries';
import axios from 'axios';

/*export function fetchTrades() {
    return function (dispatch) {
        return .then(({ trades }) => {
            dispatch({
                type: FETCH_TRADES,
                payload: trades
            });
        });
    };
}*/

export const fetchTrades = () => async dispatch => {
    try {
        const response = await axios({
            url: 'https://localbitcoinsapi.azurewebsites.net/grahql',
            method: 'post',
            headers: {
                'Access-Control-Allow-Origin': '*'
            },
            data: {
                query: getTradesQuery
            }
        })
        dispatch({
            type: FETCH_TRADES,
            payload: response.data.data.trades.nodes
        });
    }
    catch(error){
        console.log('error')
        /*dispatch( {
            type: USERS_ERROR,
            payload: error,
        })*/
    }

}