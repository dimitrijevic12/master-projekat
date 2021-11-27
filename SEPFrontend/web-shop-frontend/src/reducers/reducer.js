import {
  GET_ITEMS,
  GET_IMAGES_FOR_ITEMS,
  GET_ITEM_BY_ID,
  LOAD_IMAGE,
  CREATE_ITEM,
  GET_ITEMS_FOR_OWNER,
  EDIT_ITEM,
  SAVE_TRANSACTION,
} from "../types/types";

const initialState = {
  items: [],
  itemsImages: [],
  item: {},
  loadedImage: "",
  itemsForOwner: [],
};

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
    case CREATE_ITEM:
      return {
        ...state,
      };
    case GET_ITEMS_FOR_OWNER:
      return {
        ...state,
        itemsForOwner: action.payload,
      };
    case EDIT_ITEM:
      return {
        ...state,
      };
    case SAVE_TRANSACTION:
      return {
        ...state,
      };
    default:
      return state;
  }
}

export default reducer;
