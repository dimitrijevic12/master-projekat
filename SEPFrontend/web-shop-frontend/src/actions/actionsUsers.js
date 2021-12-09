import {
  REGISTER_USER,
  REGISTER_USER_ERROR,
  ADMIN_REGISTRATION,
  ADMIN_REGISTRATION_ERROR,
  BANK_ADMIN_REGISTRATION,
  BANK_ADMIN_REGISTRATION_ERROR,
  EDIT_ADMIN,
  EDIT_ADMIN_ERROR,
  PSP_ADMIN_REGISTRATION,
  PSP_ADMIN_REGISTRATION_ERROR,
} from "../types/types";
import axios from "axios";

export const userRegistration = (user) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44326/api/registeredUsers",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: REGISTER_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: REGISTER_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const adminRegistration = (admin) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44326/api/admins",
      admin,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: ADMIN_REGISTRATION,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: ADMIN_REGISTRATION_ERROR,
      payload: console.log(e),
    });
  }
};

export const userLoggedIn = (user) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44326/api/users/login",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    var parts = response.data.token.split("."); // header, payload, signature
    var userInfo = JSON.parse(atob(parts[1]));
    sessionStorage.setItem("tokenWebShop", response.data.token);
    sessionStorage.setItem("userIdWebShop", userInfo.user_id);
    sessionStorage.setItem("itRoleWebShop", userInfo.itRole);
    sessionStorage.setItem(
      "roleWebShop",
      userInfo.role.substring(0, userInfo.role.length - 5)
    );
    sessionStorage.setItem("usernameWebShop", userInfo.username);
    debugger;
    return true;
  } catch (e) {
    console.log(e);
  }
};

export const bankAdminRegistration = (name) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44375/api/merchants",
      name,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json",
        },
      }
    );
    dispatch({
      type: BANK_ADMIN_REGISTRATION,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: BANK_ADMIN_REGISTRATION_ERROR,
      payload: console.log(e),
    });
  }
};

export const editAdmin = (admin) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.put(
      "https://localhost:44326/api/admins",
      admin,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: EDIT_ADMIN,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_ADMIN_ERROR,
      payload: console.log(e),
    });
  }
};

export const pspAdminRegistration = (admin) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44315/api/merchants",
      admin,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: PSP_ADMIN_REGISTRATION,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: PSP_ADMIN_REGISTRATION_ERROR,
      payload: console.log(e),
    });
  }
};
