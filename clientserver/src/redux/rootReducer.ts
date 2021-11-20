import {combineReducers} from 'redux';

import usersReducer from './redux-users';

export default combineReducers({
    users: usersReducer,
});