import {
  GET_ITEMS,
  GET_ITEMS_ERROR,
  GET_IMAGES_FOR_ITEMS,
  GET_IMAGES_FOR_ITEMS_ERROR,
  GET_ITEM_BY_ID,
  GET_ITEM_BY_ID_ERROR,
  LOAD_IMAGE,
  CREATE_ITEM,
  CREATE_ITEM_ERROR,
  GET_ITEMS_FOR_OWNER,
  GET_ITEMS_FOR_OWNER_ERROR,
  EDIT_ITEM,
  EDIT_ITEM_ERROR,
} from "../types/types";
import axios from "axios";

export const getItems = () => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}items`, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
      },
    });
    dispatch({
      type: GET_ITEMS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ITEMS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getImagesForItems = (items) => async (dispatch) => {
  try {
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}contents/images`,
      items,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_IMAGES_FOR_ITEMS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_IMAGES_FOR_ITEMS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getItemById = (id) => async (dispatch) => {
  try {
    debugger;
    const response = await axios
      .get(`${process.env.REACT_APP_API_URL}items/` + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_ITEM_BY_ID,
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
      type: GET_ITEM_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const createItem = (item) => async (dispatch) => {
  try {
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}items`,
      item,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: CREATE_ITEM,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};

export const getItemsForOwner = (ownerId) => async (dispatch) => {
  ownerId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}items/users/` + ownerId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_ITEMS_FOR_OWNER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ITEMS_FOR_OWNER_ERROR,
      payload: console.log(e),
    });
  }
};

export const editItem = (item) => async (dispatch) => {
  try {
    const response = await axios.put(
      `${process.env.REACT_APP_API_URL}items`,
      item,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: EDIT_ITEM,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};
