import { GET_MERCHANT_BY_MERCHANTID, GET_PSPREQUEST } from "../types/types";

const initialState = { pspRequest: {}, merchant: {} };

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
    default:
      return state;
  }
}

export default reducer;
