import {
  GET_CONFERENCES,
  GET_CONFERENCES_ERROR,
  CREATE_CONFERENCE,
  CREATE_CONFERENCE_ERROR,
  GET_CONFERENCE_BY_ID,
  GET_CONFERENCE_BY_ID_ERROR,
  LOAD_IMAGE,
  EDIT_CONFERENCE,
  EDIT_CONFERENCE_ERROR,
  GET_CONFERENCES_FOR_OWNER,
  GET_CONFERENCES_FOR_OWNER_ERROR,
} from "../types/types";
import axios from "axios";

export const getConferences = () => async (dispatch) => {
  try {
    const response = await axios.get(`https://localhost:5001/api/conferences`, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
      },
    });
    dispatch({
      type: GET_CONFERENCES,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_CONFERENCES_ERROR,
      payload: console.log(e),
    });
  }
};

export const createConference = (conference) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:5001/api/conferences",
      conference,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: CREATE_CONFERENCE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_CONFERENCE_ERROR,
      payload: console.log(e),
    });
  }
};

export const getConferenceById = (id) => async (dispatch) => {
  try {
    debugger;
    const response = await axios
      .get("https://localhost:5001/api/conferences/" + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_CONFERENCE_BY_ID,
          payload: response.data,
        });
        const response2 = axios
          .get(
            "https://localhost:5001/api/contents/" + response.data.imagePath,
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
      type: GET_CONFERENCE_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const editConference = (conference) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:5001/api/conferences",
      conference,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: EDIT_CONFERENCE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_CONFERENCE_ERROR,
      payload: console.log(e),
    });
  }
};

export const getConferencesForOwner = (ownerId) => async (dispatch) => {
  ownerId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `https://localhost:5001/api/conferences/users/` + ownerId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_CONFERENCES_FOR_OWNER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_CONFERENCES_FOR_OWNER_ERROR,
      payload: console.log(e),
    });
  }
};
