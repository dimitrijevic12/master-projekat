import {
  GET_MERCHANT_BY_MERCHANTID,
  GET_PSPREQUEST,
  POST_TRANSACTION,
  PUSH_URLS,
} from "../types/types";

const initialState = {
  pspRequest: {},
  merchant: {},
  successUrl: "",
  errorUrl: "",
  failedUrl: "",
  transactionId: "",
};

function reducer(state = initialState, action) {
  switch (action.type) {
    case GET_PSPREQUEST:
      return {
        ...state,
        pspRequest: action.payload,
      };
    case GET_MERCHANT_BY_MERCHANTID:
      return {
        ...state,
        merchant: action.payload,
      };
    case PUSH_URLS:
      debugger;
      return {
        ...state,
        successUrl: action.payload.successUrl,
        // errorUrl: action.payload.errorUrl,
        // failedUrl: action.payload.failedUrl,
      };
    case POST_TRANSACTION:
      return {
        ...state,
        transactionId: action.payload.id,
      };
    default:
      return state;
  }
}

export default reducer;
