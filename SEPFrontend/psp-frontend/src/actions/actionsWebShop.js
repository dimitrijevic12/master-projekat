import {
    REGISTER_WEBSHOP,
    REGISTER_WEBSHOP_ERROR,
    GET_WEBSHOP,
    GET_WEBSHOP_ERROR,
    CHANGE_PAYMENTTYPES,
    CHANGE_PAYMENTTYPES_ERROR,
    GET_PAYMENTTYPES,
    GET_PAYMENTTYPES_ERROR,
    GET_PAYMENTTYPES_FOR_WEBSHOP,
    GET_PAYMENTTYPES_FOR_WEBSHOP_ERROR
  } from "../types/types";
  import axios from "axios";

export const webShopRegistration = (webShop) => async (dispatch) => {
    debugger;
    try {
        const response = await axios.post(
        "https://localhost:44390/api/RegisteredWebShops",
        webShop,
        {
            headers: {
            "Access-Control-Allow-Origin": "*",
            },
        }
        );
        dispatch({
        type: REGISTER_WEBSHOP,
        payload: response.data,
        });
        return true;
    } catch (e) {
        dispatch({
        type: REGISTER_WEBSHOP_ERROR,
        payload: console.log(e),
        });
    }
};

export const userLoggedIn = (webShop) => async (dispatch) => {
    try {
      const response = await axios.post(
        "https://localhost:44390/api/RegisteredWebShops/login",
        webShop,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
          },
        }
      );
      var parts = response.data.token.split("."); // header, payload, signature
      var webShopInfo = JSON.parse(atob(parts[1]));
      sessionStorage.setItem("tokenPSP", response.data.token);
      sessionStorage.setItem("userIdPSP", webShopInfo.webshop_id);
      sessionStorage.setItem(
        "rolePSP",
        webShopInfo.role.substring(0, webShopInfo.role.length - 5)
      );
      sessionStorage.setItem("namePSP", webShopInfo.name);
      sessionStorage.setItem("emailPSP", webShopInfo.email);
      debugger;
      return true;
    } catch (e) {
      console.log(e);
    }
  };

  export const getWebShop = (webShopEmail) => async (dispatch) => {
    webShopEmail = sessionStorage.getItem("emailPSP");
    debugger;
    try {
      const response = await axios.get(
        "https://localhost:44390/api/RegisteredWebShops/" + webShopEmail,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
          },
        }
      );
      dispatch({
        type: GET_WEBSHOP,
        payload: response.data,
      });
    } catch (e) {
        debugger;
      dispatch({
        type: GET_WEBSHOP_ERROR,
        payload: console.log(e),
      });
    }
  };

  export const changePaymentTypes = (paymentTypes) => async (dispatch) => {
    debugger;
    try {
        const response = await axios.put(
        "https://localhost:44390/api/RegisteredWebShops",
        paymentTypes,
        {
            headers: {
            "Access-Control-Allow-Origin": "*",
            },
        }
        );
        dispatch({
        type: CHANGE_PAYMENTTYPES,
        payload: response.data,
        });
        return true;
    } catch (e) {
        dispatch({
        type: CHANGE_PAYMENTTYPES_ERROR,
        payload: console.log(e),
        });
    }
};

export const getPaymentTypes = () => async (dispatch) => {  
    try {
      const response = await axios.get(
        "https://localhost:44390/api/PaymentTypes",
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
          },
        }
      );
      dispatch({
        type: GET_PAYMENTTYPES,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_PAYMENTTYPES_ERROR,
        payload: console.log(e),
      });
    }
  };

  export const getPaymentTypesForWebShop = (orderId) => async (dispatch) => {
    debugger;
    try {
      const response = await axios.get(
        "https://localhost:44390/api/paymenttypes/" + orderId,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
          },
        }
      );
      dispatch({
        type: GET_PAYMENTTYPES_FOR_WEBSHOP,
        payload: response.data,
      });
    } catch (e) {
        debugger;
      dispatch({
        type: GET_PAYMENTTYPES_FOR_WEBSHOP_ERROR,
        payload: console.log(e),
      });
    }
  };