import { useContext, useMemo } from "react";
import { bindActionCreators } from "redux";
import { ReactReduxContext, useSelector } from "react-redux";
import {loadUsers} from './actions';

/**
 * dá a indicação da store (contentor com o estado da aplicação)
 * que é usada, para cada store tem de haver um reducer
 */
export default() => {
    const {
        store: {dispatch},
    } = useContext(ReactReduxContext);

    const users = useSelector(state => state.users) || {};

    const actions = useMemo(
        () => bindActionCreators(
            {
                loadUsers,
            },
            dispatch
        ) ,
        [dispatch]
    );

    return {
        ...actions,
        users: users.result,
        isLoading: users.isLoading,
        error: users.error,
    };
};