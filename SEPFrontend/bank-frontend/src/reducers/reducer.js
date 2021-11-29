import { GET_PSPREQUEST } from "../types/types";

const initialState = { pspRequest: {} };

function reducer(state = initialState, action) {
  switch (action.type) {
    case GET_PSPREQUEST:
      return {
        ...state,
        pspRequest: action.payload,
      };
  }
}

export default reducer;
