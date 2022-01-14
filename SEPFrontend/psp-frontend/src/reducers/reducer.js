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
  SET_PAYPAL_TRANSACTION_STATUS,
  GET_PAYPAL_TRANSACTION,
  SET_CRYPTO_TRANSACTION_STATUS,
  PAY_WITH_CRYPTO_VALUTE,
  GET_CRYPTO_TRANSACTION,
} from "../types/types";

const initialState = {
  registeredWebShop: {},
  changedPaymentTypes: {},
  paymentTypes: [],
  transactions: [],
  request: {},
  payment: {},
  paypalTransaction: {},
  cryptoValutePayment: {},
  cryptoTransaction: {},
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
      };
    case GET_PAYMENTTYPES_FOR_WEBSHOP:
      return {
        ...state,
        paymentTypes: action.payload,
      };
    case SET_PAYPAL_TRANSACTION_STATUS:
      return {
        ...state,
      };
    case GET_PAYPAL_TRANSACTION:
      return {
        ...state,
        paypalTransaction: action.payload,
      };
    case SET_CRYPTO_TRANSACTION_STATUS:
      return {
        ...state,
      };
    case PAY_WITH_CRYPTO_VALUTE:
      return {
        ...state,
        cryptoValutePayment: action.payload,
      };    
    case GET_CRYPTO_TRANSACTION:
    return {
      ...state,
      cryptoTransaction: action.payload,
    };   
    default:
      return state;
  }
}

export default reducer;
