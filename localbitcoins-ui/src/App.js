import TradesGrid from './components/TradesGrid';
import { Provider } from 'react-redux';
import store from './store';

function App() {
  return (
    <Provider store ={store}>
      <div className="App">
        <TradesGrid />
      </div>
    </Provider>
  );
}

export default App;
