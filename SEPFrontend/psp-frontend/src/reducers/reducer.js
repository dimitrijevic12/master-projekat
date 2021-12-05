import {
    REGISTER_WEBSHOP,
    GET_WEBSHOP,
    CHANGE_PAYMENTTYPES,
    GET_PAYMENTTYPES,
    GET_TRANSACTIONS,
    GET_REQUEST,
    SEND_REQUEST,
    SET_PAYMENT_ID,
    GET_PAYMENTTYPES_FOR_WEBSHOP,
  } from "../types/types";

  const initialState = {

    registeredWebShop: {},
    changedPaymentTypes: {},
    paymentTypes: [], 
    transactions: [], 
    request: {},
    payment: {},
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
      case GET_REQUEST:
      return {
        ...state,
        request: action.payload,
      }; 
    case SEND_REQUEST:
      return {
        ...state,
        payment: action.payload,
      }; 
    case SET_PAYMENT_ID:
      return {
        ...state,
        payment: action.payload,
      }; 
    case GET_PAYMENTTYPES_FOR_WEBSHOP:
      return {
        ...state,
        paymentTypes: action.payload,
      };  
      
      default:
        return state;
    }
  }
  
  export default reducer;