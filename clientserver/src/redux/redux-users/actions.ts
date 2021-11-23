import axios from 'axios';
import get from 'lodash/get';

//import {}
import * as actionTypes from './actionTypes';

export const loadUsers = () => async (dispatch, getState) => {
    const state = getState();
    const shouldFetch = state.users.shouldFetch;

    if(!shouldFetch)return;

    dispatch({
        type: actionTypes.LOAD_USERS_START
    });

    try{
        //ver como é aqui, adaptar para o nosso código
        const res = await axios({
            method: 'get',
           // url:
           headers: {Authorization: get(state, 'auth.profile.result.token')},
        });

        dispatch({
            type: actionTypes.LOAD_USERS_SUCCESS,
            payload: res.data,
        });
    } catch (error){
        dispatch({
            type: actionTypes.LOAD_USERS_ERROR,
            payload: error,
        });
    }
    };
