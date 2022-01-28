import {
  GET_ITEMS,
  GET_IMAGES_FOR_ITEMS,
  GET_ITEM_BY_ID,
  LOAD_IMAGE,
  CREATE_ITEM,
  GET_ITEMS_FOR_OWNER,
  EDIT_ITEM,
  SAVE_TRANSACTION,
  REGISTER_USER,
  ADMIN_REGISTRATION,
  GET_CONFERENCES,
  CREATE_CONFERENCE,
  GET_CONFERENCE_BY_ID,
  EDIT_CONFERENCE,
  GET_COURSES,
  GET_COURSE_BY_ID,
  CREATE_COURSE,
  EDIT_COURSE,
  GET_COURSES_FOR_OWNER,
  GET_CONFERENCES_FOR_OWNER,
  GET_TRANSACTIONS_FOR_BUYER,
  GET_TRANSACTION_BY_ID,
  CREATE_ACCOMMODATION,
  GET_ACCOMMODATIONS_FOR_CITY,
  GET_ACCOMMODATION_BY_ID,
  GET_ACCOMMODATIONS_FOR_OWNER,
  GET_TRANSACTIONS_FOR_SELLER,
  EDIT_ACCOMMODATION,
  GET_TRANSPORTATIONS,
  CREATE_TRANSPORTATION,
  GET_TRANSPORTATION_BY_ID,
  GET_TRANSPORTATIONS_FOR_OWNER,
  EDIT_TRANSPORTATION,
  BANK_ADMIN_REGISTRATION,
  EDIT_ADMIN,
  PSP_ADMIN_REGISTRATION,
  SEND_TRANSACTION_TO_PSP,
  PAY_PERDIEM,
  GET_REGISTERED_USER_BY_ID,
  EDIT_PERDIEM_STATUS,
} from "../types/types";

const initialState = {
  items: [],
  itemsImages: [],
  item: {},
  loadedImage: "",
  itemsForOwner: [],
  registeredUser: {},
  conferences: [],
  conference: {},
  courses: [],
  course: {},
  coursesForOwner: [],
  conferencesForOwner: [],
  transactionsForBuyer: [],
  transaction: {},
  accommodationsForCity: [],
  accommodation: {},
  accommodationsForOwner: [],
  transactionsForSeller: [],
  transportations: [],
  transportation: {},
  transportationsForOwner: [],
  bankAdmin: {},
  admin: {},
  savedTransaction: {},
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
        savedTransaction: action.payload,
      };
    case REGISTER_USER:
      return {
        ...state,
        registeredUser: action.payload,
      };
    case ADMIN_REGISTRATION:
      return {
        ...state,
        admin: action.payload,
      };
    case GET_CONFERENCES:
      return {
        ...state,
        conferences: action.payload,
      };
    case CREATE_CONFERENCE:
      return {
        ...state,
      };
    case GET_CONFERENCE_BY_ID:
      return {
        ...state,
        conference: action.payload,
      };
    case EDIT_CONFERENCE:
      return {
        ...state,
      };
    case GET_COURSES:
      return {
        ...state,
        courses: action.payload,
      };
    case GET_COURSE_BY_ID:
      return {
        ...state,
        course: action.payload,
      };
    case CREATE_COURSE:
      return {
        ...state,
      };
    case EDIT_COURSE:
      return {
        ...state,
      };
    case GET_COURSES_FOR_OWNER:
      return {
        ...state,
        coursesForOwner: action.payload,
      };
    case GET_CONFERENCES_FOR_OWNER:
      return {
        ...state,
        conferencesForOwner: action.payload,
      };
    case GET_TRANSACTIONS_FOR_BUYER:
      return {
        ...state,
        transactionsForBuyer: action.payload,
      };
    case GET_TRANSACTION_BY_ID:
      return {
        ...state,
        transaction: action.payload,
      };
    case CREATE_ACCOMMODATION:
      return {
        ...state,
      };
    case GET_ACCOMMODATIONS_FOR_CITY:
      return {
        ...state,
        accommodationsForCity: action.payload,
      };
    case GET_ACCOMMODATION_BY_ID:
      return {
        ...state,
        accommodation: action.payload,
      };
    case GET_ACCOMMODATIONS_FOR_OWNER:
      return {
        ...state,
        accommodationsForOwner: action.payload,
      };
    case GET_TRANSACTIONS_FOR_SELLER:
      return {
        ...state,
        transactionsForSeller: action.payload,
      };
    case EDIT_ACCOMMODATION:
      return {
        ...state,
      };
    case GET_TRANSPORTATIONS:
      return {
        ...state,
        transportations: action.payload,
      };
    case CREATE_TRANSPORTATION:
      return {
        ...state,
      };
    case GET_TRANSPORTATION_BY_ID:
      return {
        ...state,
        transportation: action.payload,
      };
    case GET_TRANSPORTATIONS_FOR_OWNER:
      return {
        ...state,
        transportationsForOwner: action.payload,
      };
    case EDIT_TRANSPORTATION:
      return {
        ...state,
      };
    case BANK_ADMIN_REGISTRATION:
      return {
        ...state,
        bankAdmin: action.payload,
      };
    case EDIT_ADMIN:
      return {
        ...state,
      };
    case PSP_ADMIN_REGISTRATION:
      return {
        ...state,
      };
    case SEND_TRANSACTION_TO_PSP:
      return {
        ...state,
      };
    case PAY_PERDIEM:
      return {
        ...state,
      };
    case GET_REGISTERED_USER_BY_ID:
      return {
        ...state,
        registeredUser: action.payload,
      };
    case EDIT_PERDIEM_STATUS:
      return {
        ...state,
      };
    default:
      return state;
  }
}

export default reducer;
