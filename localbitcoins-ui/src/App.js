import Trades from './components/Trades';
import { Provider } from 'react-redux';
import store from './store';

function App() {
  return (
    <Provider store ={store}>
      <div className="App">
        <Trades></Trades>
      </div>
    </Provider>
  );
}

export default App;
