import { configureStore } from "@reduxjs/toolkit";
import  useReducer from "../redux/redux-users/reducer";

export default configureStore({
  reducer: {
    user: useReducer
  }
})
