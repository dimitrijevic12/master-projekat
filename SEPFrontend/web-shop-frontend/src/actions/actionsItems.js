import {
  GET_ITEMS,
  GET_ITEMS_ERROR,
  GET_IMAGES_FOR_ITEMS,
  GET_IMAGES_FOR_ITEMS_ERROR,
  GET_ITEM_BY_ID,
  GET_ITEM_BY_ID_ERROR,
  LOAD_IMAGE,
  LOAD_IMAGE_ERROR,
} from "../types/types";
import axios from "axios";

export const getItems = () => async (dispatch) => {
  try {
    const response = await axios.get(`https://localhost:5001/api/items`, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        //Authorization: "Bearer " + sessionStorage.getItem("token"),
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
      "https://localhost:5001/api/contents/images",
      items,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("token"),
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
      .get("https://localhost:5001/api/items/" + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_ITEM_BY_ID,
          payload: response.data,
        });
        const response2 = axios
          .get(
            "https://localhost:5001/api/contents/" + response.data.imagePath,
            {
              headers: {
                "Access-Control-Allow-Origin": "*",
                //Authorization:
                //  "Bearer " + sessionStorage.getItem("tokenAgentApp"),
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
