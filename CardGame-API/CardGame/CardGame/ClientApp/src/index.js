import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import rootReducer from './reducers/rootReducer';
import { createStore } from 'redux';
import { Provider } from 'react-redux';
import { CheckLoggedIn } from './AuthenticationStatusManager/AuthenticationStatusManager';

const rootElement = document.getElementById("root");


const store = createStore(rootReducer, CheckLoggedIn());

ReactDOM.render(
    <Provider store={store}>
        <App />
    </Provider>,
    rootElement);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
