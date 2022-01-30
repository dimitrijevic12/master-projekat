import {
  CREATE_ACCOMMODATION,
  CREATE_ACCOMMODATION_ERROR,
  GET_ACCOMMODATIONS_FOR_CITY,
  GET_ACCOMMODATIONS_FOR_CITY_ERROR,
  GET_ACCOMMODATION_BY_ID,
  GET_ACCOMMODATION_BY_ID_ERROR,
  LOAD_IMAGE,
  GET_ACCOMMODATIONS_FOR_OWNER,
  GET_ACCOMMODATIONS_FOR_OWNER_ERROR,
  EDIT_ACCOMMODATION,
  EDIT_ACCOMMODATION_ERROR,
} from "../types/types";
import axios from "axios";

export const createAccommodation = (accommodation) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}accommodations`,
      accommodation,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: CREATE_ACCOMMODATION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_ACCOMMODATION_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAccommodationsForCity = (city) => async (dispatch) => {
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}accommodations?`,
      {
        params: { city: city },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_ACCOMMODATIONS_FOR_CITY,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ACCOMMODATIONS_FOR_CITY_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAccommodationById = (id) => async (dispatch) => {
  try {
    debugger;
    const response = await axios
      .get(`${process.env.REACT_APP_API_URL}accommodations/` + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_ACCOMMODATION_BY_ID,
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
      type: GET_ACCOMMODATION_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAccommodationsForOwner = (ownerId) => async (dispatch) => {
  ownerId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}accommodations/users/` + ownerId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_ACCOMMODATIONS_FOR_OWNER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ACCOMMODATIONS_FOR_OWNER_ERROR,
      payload: console.log(e),
    });
  }
};

export const editAccommodation = (accommodation) => async (dispatch) => {
  try {
    const response = await axios.put(
      `${process.env.REACT_APP_API_URL}accommodations`,
      accommodation,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: EDIT_ACCOMMODATION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_ACCOMMODATION_ERROR,
      payload: console.log(e),
    });
  }
};
