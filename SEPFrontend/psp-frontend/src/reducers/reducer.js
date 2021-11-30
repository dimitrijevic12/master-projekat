import {
    REGISTER_WEBSHOP,
  } from "../types/types";

  const initialState = {

    registeredWebShop: {},
  };

  function reducer(state = initialState, action) {
    switch (action.type) {
      case REGISTER_WEBSHOP:
        return {
          ...state,
          registeredWebShop: action.payload,
        };
      default:
        return state;
    }
  }
  
  export default reducer;