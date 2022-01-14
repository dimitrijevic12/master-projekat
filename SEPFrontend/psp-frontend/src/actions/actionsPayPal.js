import {
  SET_PAYPAL_TRANSACTION_STATUS,
  SET_PAYPAL_TRANSACTION_STATUS_ERROR,
  GET_PAYPAL_TRANSACTION,
  GET_PAYPAL_TRANSACTION_ERROR,
} from "../types/types";
import axios from "axios";

export const setPayPalTransactionStatus =
  (transactionStatus) => async (dispatch) => {
    debugger;
    try {
      const response = await axios.put(
        "https://localhost:44390/api/paypal-transactions",
        transactionStatus,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
          },
        }
      );
      dispatch({
        type: SET_PAYPAL_TRANSACTION_STATUS,
        payload: response.data,
      });
      return true;
    } catch (e) {
      dispatch({
        type: SET_PAYPAL_TRANSACTION_STATUS_ERROR,
        payload: console.log(e),
      });
    }
  };

export const getPayPalTransaction = (orderId) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(
      "https://localhost:44390/api/paypal-transactions/" + orderId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
        },
      }
    );
    dispatch({
      type: GET_PAYPAL_TRANSACTION,
      payload: response.data,
    });
  } catch (e) {
    debugger;
    dispatch({
      type: GET_PAYPAL_TRANSACTION_ERROR,
      payload: console.log(e),
    });
  }
};
