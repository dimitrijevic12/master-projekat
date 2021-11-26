import { GET_ITEMS } from "../types/types";

const initialState = { items: [] };

function reducer(state = initialState, action) {
  switch (action.type) {
    case GET_ITEMS:
      return {
        ...state,
        items: action.payload,
      };
    default:
      return state;
  }
}

export default reducer;
