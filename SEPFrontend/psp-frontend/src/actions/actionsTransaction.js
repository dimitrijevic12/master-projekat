import {
  GET_TRANSACTIONS,
  GET_TRANSACTIONS_ERROR,
  GET_REQUEST,
  GET_REQUEST_ERROR,
  SEND_REQUEST,
  SEND_REQUEST_ERROR,
  SET_PAYMENT_ID,
  SET_PAYMENT_ID_ERROR,
} from "../types/types";
import axios from "axios";

export const getTransactions = () => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(
      "http://localhost:60212/api/Transactions",
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
        },
      }
    );
    dispatch({
      type: GET_TRANSACTIONS,
      payload: response.data,
    });
  } catch (e) {
    debugger;
    dispatch({
      type: GET_TRANSACTIONS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getRequest = (orderId) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(
      "http://localhost:60212/api/Transactions/" + orderId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
        },
      }
    );
    dispatch({
      type: GET_REQUEST,
      payload: response.data,
    });
  } catch (e) {
    debugger;
    dispatch({
      type: GET_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const sendRequest = (request) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44375/api/PSPRequests",
      request,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: SEND_REQUEST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: SEND_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const setPaymentId = (transactionsPayment) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.put(
      "http://localhost:60212/api/Transactions/paymentId",
      transactionsPayment,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      }
    );
    dispatch({
      type: SET_PAYMENT_ID,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: SET_PAYMENT_ID_ERROR,
      payload: console.log(e),
    });
  }
};
