import {
  GET_COURSES,
  GET_COURSES_ERROR,
  GET_COURSE_BY_ID,
  LOAD_IMAGE,
  GET_COURSE_BY_ID_ERROR,
  CREATE_COURSE,
  CREATE_COURSE_ERROR,
  EDIT_COURSE,
  EDIT_COURSE_ERROR,
  GET_COURSES_FOR_OWNER,
  GET_COURSES_FOR_OWNER_ERROR,
} from "../types/types";
import axios from "axios";

export const getCourses = () => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}courses`,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_COURSES,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_COURSES_ERROR,
      payload: console.log(e),
    });
  }
};

export const getCourseById = (id) => async (dispatch) => {
  try {
    debugger;
    const response = await axios
      .get(`${process.env.REACT_APP_API_URL}courses/` + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_COURSE_BY_ID,
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
      type: GET_COURSE_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const createCourse = (course) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}courses`,
      course,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: CREATE_COURSE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_COURSE_ERROR,
      payload: console.log(e),
    });
  }
};

export const editCourse = (course) => async (dispatch) => {
  try {
    const response = await axios.put(
      `${process.env.REACT_APP_API_URL}courses`,
      course,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: EDIT_COURSE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_COURSE_ERROR,
      payload: console.log(e),
    });
  }
};

export const getCoursesForOwner = (ownerId) => async (dispatch) => {
  ownerId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}courses/users/` + ownerId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_COURSES_FOR_OWNER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_COURSES_FOR_OWNER_ERROR,
      payload: console.log(e),
    });
  }
};
