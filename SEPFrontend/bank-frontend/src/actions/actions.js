import {
  GET_MERCHANT_BY_MERCHANTID,
  GET_MERCHANT_BY_MERCHANTID_ERROR,
  GET_PSPREQUEST,
  GET_PSPREQUEST_ERROR,
  GET_TRANSACTION_STATUS,
  GET_TRANSACTION_STATUS_ERROR,
  POST_TRANSACTION,
  POST_TRANSACTION_ERROR,
  PUSH_URLS,
} from "../types/types";
import axios from "axios";
import { push } from "react-router-redux";

export const getPSPRequest = (paymentId) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}psprequests`,
      {
        params: { paymentId: paymentId },

        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    debugger;
    dispatch({
      type: GET_PSPREQUEST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_PSPREQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const postTransaction = (cardInfo) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}Transactions`,
      {
        PaymentId: cardInfo.PaymentId,
        PAN: cardInfo.PAN,
        SecurityCode: cardInfo.SecurityCode,
        CardHolderName: cardInfo.CardHolderName,
        ExpirationDate: cardInfo.ExpirationDate,
        Amount: cardInfo.Amount,
        AcquirerAccountNumber: cardInfo.AcquirerAccountNumber,
        AcquirerName: cardInfo.acquirerName,
      },
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    debugger;
    if (response.data.transactionStatus === "Success")
      window.location.replace(cardInfo.successUrl);
    await dispatch({
      type: POST_TRANSACTION,
      payload: response.data,
    });
    await dispatch({
      type: PUSH_URLS,
      payload: {
        successUrl: cardInfo.successUrl,
        errorUrl: cardInfo.errorUrl,
        failedUrl: cardInfo.failedUrl,
      },
    });
    return `pending/${response.data.transactionId}`;
  } catch (e) {
    debugger;
    if (e.response.data.transactionStatus === "Failed")
      window.location.replace(cardInfo.failedUrl);
    else return window.location.replace(cardInfo.errorUrl);
  }
};

export const getMerchantByMerchantId = (merchantId) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}merchants`,
      {
        params: { merchantId: merchantId },

        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    dispatch({
      type: GET_MERCHANT_BY_MERCHANTID,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_MERCHANT_BY_MERCHANTID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTransactionStatus =
  (transactionId, successUrl, errorUrl, failedUrl) => async (dispatch) => {
    try {
      debugger;
      const response = await axios.get(
        `${process.env.REACT_APP_API_URL}transactions/${transactionId}/transaction-status`,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
          },
        }
      );
      if (response.data.status === "Pending") {
        return true;
      } else if (response.data.status === "Success")
        window.location.replace(successUrl);
      else if (response.data.status === "Failed")
        window.location.replace(failedUrl);
      else window.location.replace(errorUrl);
      dispatch({
        type: GET_TRANSACTION_STATUS,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_TRANSACTION_STATUS_ERROR,
        payload: console.log(e),
      });
    }
  };
