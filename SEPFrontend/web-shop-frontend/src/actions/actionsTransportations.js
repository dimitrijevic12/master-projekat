import {
  GET_TRANSPORTATIONS,
  GET_TRANSPORTATIONS_ERROR,
  CREATE_TRANSPORTATION,
  CREATE_TRANSPORTATION_ERROR,
  GET_TRANSPORTATION_BY_ID,
  GET_TRANSPORTATION_BY_ID_ERROR,
  LOAD_IMAGE,
  GET_TRANSPORTATIONS_FOR_OWNER,
  GET_TRANSPORTATIONS_FOR_OWNER_ERROR,
  EDIT_TRANSPORTATION,
  EDIT_TRANSPORTATION_ERROR,
} from "../types/types";
import axios from "axios";

export const getTransportations =
  (startDestination, finalDestination) => async (dispatch) => {
    try {
      const response = await axios.get(
        `${process.env.REACT_APP_API_URL}transportations?`,
        {
          params: {
            startDestination: startDestination,
            finalDestination: finalDestination,
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
          },
        }
      );
      dispatch({
        type: GET_TRANSPORTATIONS,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_TRANSPORTATIONS_ERROR,
        payload: console.log(e),
      });
    }
  };

export const createTransportation = (transportation) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}transportations`,
      transportation,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: CREATE_TRANSPORTATION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_TRANSPORTATION_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTransportationById = (id) => async (dispatch) => {
  try {
    debugger;
    const response = await axios
      .get(`${process.env.REACT_APP_API_URL}transportations/` + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_TRANSPORTATION_BY_ID,
          payload: response.data,
        });
        const response2 = axios
          .get(
            `${process.env.REACT_APP_API_URL}contents/` +
              response.data.imagePath,
            {
              headers: {
                "Access-Control-Allow-Origin": "*",
                Authorization:
                  "Bearer " + sessionStorage.getItem("tokenWebShop"),
              },
            }
          )
          .then(function (response2) {
            dispatch({
              type: LOAD_IMAGE,
              payload: response2.data,
            });
          });
      });
  } catch (e) {
    dispatch({
      type: GET_TRANSPORTATION_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTransportationsForOwner = (ownerId) => async (dispatch) => {
  ownerId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}transportations/users/` + ownerId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_TRANSPORTATIONS_FOR_OWNER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_TRANSPORTATIONS_FOR_OWNER_ERROR,
      payload: console.log(e),
    });
  }
};

export const editTransportation = (transportation) => async (dispatch) => {
  try {
    const response = await axios.put(
      `${process.env.REACT_APP_API_URL}transportations`,
      transportation,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: EDIT_TRANSPORTATION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_TRANSPORTATION_ERROR,
      payload: console.log(e),
    });
  }
};
