import {
    REGISTER_WEBSHOP,
    GET_WEBSHOP,
    CHANGE_PAYMENTTYPES,
    GET_PAYMENTTYPES,
    GET_TRANSACTIONS,
  } from "../types/types";

  const initialState = {

    registeredWebShop: {},
    changedPaymentTypes: {},
    paymentTypes: [], 
    transactions: [], 
  };

  function reducer(state = initialState, action) {
    switch (action.type) {
      case REGISTER_WEBSHOP:
        return {
          ...state,
          registeredWebShop: action.payload,
        };
      case GET_WEBSHOP:
        return {
          ...state,
          registeredWebShop: action.payload,
        };
      case CHANGE_PAYMENTTYPES:
        return {
          ...state,
          changedPaymentTypes: action.payload,
        };
      case GET_PAYMENTTYPES:
        return {
          ...state,
          paymentTypes: action.payload,
        };
      case GET_TRANSACTIONS:
        return {
          ...state,
          transactions: action.payload,
        };       
      default:
        return state;
    }
  }
  
  export default reducer;