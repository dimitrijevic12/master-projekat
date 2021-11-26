import {
  GET_ITEMS,
  GET_IMAGES_FOR_ITEMS,
  GET_ITEM_BY_ID,
  LOAD_IMAGE,
} from "../types/types";

const initialState = { items: [], itemsImages: [], item: {}, loadedImage: "" };

function reducer(state = initialState, action) {
  switch (action.type) {
    case GET_ITEMS:
      return {
        ...state,
        items: action.payload,
      };
    case GET_IMAGES_FOR_ITEMS:
      return {
        ...state,
        itemsImages: action.payload,
      };
    case GET_ITEM_BY_ID:
      return {
        ...state,
        item: action.payload,
      };
    case LOAD_IMAGE:
      return {
        ...state,
        loadedImage: action.payload,
      };
    default:
      return state;
  }
}

export default reducer;
