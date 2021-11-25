import { applyMiddleware, compose, createStore } from "redux";
import ThunkMiddleware from "redux-thunk";
import { composeWithDevTools } from "@reduxjs/toolkit/dist/devtoolsExtension";
import monitorReducerEnhancer from "../enhancers/monitorReducer";
import loggerMiddleware from '../middleware/logger'
import rootReducer from "./rootReducer";

export default function configureStore(preloadedState) {
    const middlewares = [loggerMiddleware, thunkMiddleware]
    const middlewareEnhancer = applyMiddleware(...middlewares)

    const enhancers = [middlewareEnhancer, monitorReducerEnhancer]
    const composedEnhancers = composeWithDevTools(...enhancers)

    
    const store = createStore(rootReducer, preloadedState, composedEnhancers)
    return store
}